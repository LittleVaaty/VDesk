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
    debug!("SetNameArgs: name={}, index={}", args.name, args.index);

    let desktops = desktop_utils::get_desktops()?;
    
    let index = usize::try_from(args.index - 1)
        .map_err(|_| eyre!("Desktop index must be positive"))?;

    let desktop = desktops.get(index)
        .ok_or_else(|| eyre!("Desktop index {} out of bounds", args.index))?;

    desktop.set_name(&args.name)
        .map_err(|e| eyre!("Failed to set desktop name: {:?}", e))
}