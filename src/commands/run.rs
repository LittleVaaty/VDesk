use clap::{Args};
use color_eyre::eyre::{Result, eyre};
use std::process::{Command, Stdio};
use std::thread;
use std::time::Duration;
use log::{debug, info, trace};
use winvd::{get_desktops, move_window_to_desktop, switch_desktop, Desktop};


use crate::commands::common::{self, HalfSplit};
use crate::utils::window_utils::{find_main_window, move_half_split};

/// Options for the `run` command
#[derive(Args, Debug)]
pub struct RunArgs {
    /// Virtual desktop on which to run the command (name or index)
    #[arg(short = 'o', long = "on", required = true)]
    pub index_or_name: String,
    
    /// Command to execute
    pub command: String,
    
    /// Command arguments
    #[arg(short = 'a', long = "arguments")]
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
    debug_args(&args);

    let desktops: Vec<Desktop> = get_desktops()
        .map_err(|e| eyre!("Failed to retrieve virtual desktops: {:?}", e))?;

    let desktop: Desktop = common::get_desktop_id_by_name_or_index(desktops, &args.index_or_name)
        .ok_or_else(|| eyre!("Virtual desktop not found: {}", &args.index_or_name))?;
    
    if !args.no_switch {
        trace!("Switching to virtual desktop: {}", args.index_or_name);
        switch_desktop(desktop)
            .map_err(|e| eyre!("Failed to switch to virtual desktop: {:?}", e))?;
    }

    let pid = launch_process(&args.command, args.arguments.clone())?;

    
    // Small delay to let the window open
    let delay = args.waiting.unwrap_or(500);
    trace!("Waiting {} ms before searching for the window", delay);
    thread::sleep(Duration::from_millis(delay));

    let hwnd = find_main_window(pid).ok_or_else(|| eyre!("Failed to find main window for PID {}", pid))?;
    trace!("hwnd: {:?}", hwnd);

    if args.no_switch {
        move_window_to_desktop(desktop, &hwnd)
        .map_err(|e| eyre!("Failed to move the window to the virtual desktop: {:?}", e))?;
    }

    move_half_split(hwnd, args.half_split)
        .map_err(|e| eyre!("Failed to move the window to the specified half of the screen: {:?}", e))
}

fn launch_process(exe: &str, args: Option<String>) -> Result<u32> {
    let mut cmd = Command::new(exe);
    if let Some(a) = args {
        cmd.arg(a);
    }
    let child = cmd.stdin(Stdio::null())
        .stdout(Stdio::null())
        .stderr(Stdio::null())
        .spawn()
        .map_err(|e| eyre!("Failed to launch process: {:?}", e))?;
    
    trace!("Process started with PID {}", child.id());
    Ok(child.id())
}

fn debug_args(args: &RunArgs) {
    debug!("RunArgs:");
    debug!("  index_or_name: {}", args.index_or_name);
    debug!("  command: {}", args.command);
    debug!("  arguments: {:?}", args.arguments);
    debug!("  no_switch: {}", args.no_switch);
    debug!("  half_split: {:?}", args.half_split);
    debug!("  waiting: {:?}", args.waiting);
}