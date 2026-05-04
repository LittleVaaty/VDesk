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
    let desktop = desktops[(args.index - 1) as usize].clone();
    let name = desktop.get_name().map_err(|e| eyre!("Failed to get desktop name: {:?}", e))?;
    println!("Name of desktop {}: {}", args.index, name);
    Ok(())
}
