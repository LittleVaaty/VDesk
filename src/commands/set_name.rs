use clap::Args;
use color_eyre::eyre::{eyre, Result};
use log::{debug, info};

use crate::utils::desktop_utils;

#[derive(Args, Debug)]
pub struct SetNameArgs {
    name: String,

    /// Desktop on which the command is run
    #[arg(short = 'i', long = "index", required = true)]
    index: i32,
}

pub fn set_name(args: SetNameArgs) -> Result<()> {
    info!("Running the 'set-name' command");
    debug_args(&args);

    let desktops = desktop_utils::get_desktops()?;
    let desktop = desktops[(args.index - 1) as usize].clone();
    desktop.set_name(&args.name)
        .map_err(|e| eyre!("Failed to set desktop name: {:?}", e))
}

fn debug_args(args: &SetNameArgs) {
    debug!("SetNameArgs:");
    debug!("  name: {}", args.name);
    debug!("  index: {}", args.index);
}