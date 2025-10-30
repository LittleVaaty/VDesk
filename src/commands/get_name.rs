use clap::Args;
use color_eyre::eyre::{eyre, Result};
use log::info;
use winvd::{get_desktops, Desktop};

#[derive(Args, Debug)]
pub struct GetNameArgs {
    /// Desktop on which the command is run
    #[arg(short = 'o', long = "on")]
    index: i32,
}

pub fn get_name(args: GetNameArgs) -> Result<()> {
    info!("Running the 'get-name' command");

    let desktops: Vec<Desktop> = get_desktops()
        .map_err(|e| eyre!("Failed to retrieve virtual desktops: {:?}", e))?;
    let desktop = desktops[(args.index - 1) as usize].clone();
    let name = desktop.get_name().map_err(|e| eyre!("Failed to get desktop name: {:?}", e))?;
    println!("Name of desktop {}: {}", args.index, name);
    Ok(())
}
