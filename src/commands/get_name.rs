use clap::Args;
use color_eyre::eyre::{eyre, Result};
use log::info;

use crate::utils::desktop_utils;

#[derive(Args, Debug)]
pub struct GetNameArgs {
    /// Desktop on which the command is run
    #[arg(short = 'o', long = "on")]
    index: i32,
}

pub fn get_name(args: GetNameArgs) -> Result<()> {
    info!("Running the 'get-name' command");

    let desktops = desktop_utils::get_desktops()?;
    
    let index = usize::try_from(args.index - 1)
        .map_err(|_| eyre!("Desktop index must be positive"))?;

    let desktop = desktops.get(index)
        .ok_or_else(|| eyre!("Desktop index {} out of bounds", args.index))?;

    let name = desktop.get_name()
        .map_err(|e| eyre!("Failed to get desktop name: {:?}", e))?;
    
    println!("Name of desktop {}: {}", args.index, name);
    Ok(())
}
