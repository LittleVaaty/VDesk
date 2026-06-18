use clap::Args;
use color_eyre::eyre::{Result, eyre, Context};
use std::thread;
use std::time::Duration;
use log::{debug, info};

use crate::commands::common::HalfSplit;
use crate::utils::{desktop_utils, window_utils, process_utils};

const DEFAULT_WAIT_MS: u64 = 500;

/// Options for the `run` command
#[derive(Args, Debug)]
pub struct RunArgs {
    /// Virtual desktop on which to run the command (name or index)
    #[arg(short = 'o', long = "on", required = true)]
    pub index_or_name: String,
    
    /// Command to execute
    pub command: String,
    
    /// Command arguments
    #[arg(short = 'a', long = "arguments", allow_hyphen_values = true)]
    pub arguments: Option<String>,
    
    /// Do not switch to the virtual desktop
    #[arg(short = 'n', long = "no-switch")]
    pub no_switch: bool,

    /// Position the window on the specified half of the screen
    #[arg(long = "half-split", value_enum)]
    pub half_split: Option<HalfSplit>,
    
    /// Wait time after startup before moving the window in ms (default: 500 ms)
    #[arg(short = 'w', long = "waiting")]
    pub waiting: Option<u64>,
}

pub fn run(args: RunArgs) -> Result<()> {
    info!("Running the 'run' command");
    debug!("RunArgs: {:?}", args);

    let desktop = desktop_utils::get_desktop_id_by_name_or_index(&args.index_or_name)?
        .ok_or_else(|| eyre!("Virtual desktop not found: {}", &args.index_or_name))?;
    
    if !args.no_switch {
        desktop_utils::switch_desktop(&desktop)?;
    }

    let pid = process_utils::launch(&args.command, args.arguments.as_deref())?;

    let delay = args.waiting.unwrap_or(DEFAULT_WAIT_MS);
    thread::sleep(Duration::from_millis(delay));

    let hwnd = window_utils::find_main_window(pid)
        .ok_or_else(|| eyre!("Failed to find main window for PID {}", pid))?;

    if args.no_switch {
        desktop_utils::move_window_to_desktop(&desktop, &hwnd)?;
    }

    window_utils::move_half_split(hwnd, args.half_split)
        .context("Failed to move the window to the specified half of the screen")
}