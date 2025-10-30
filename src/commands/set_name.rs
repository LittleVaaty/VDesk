use clap::Args;
use color_eyre::eyre::{eyre, Result};
use log::{debug, info};
use winvd::{get_desktops, Desktop};

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

    let desktops: Vec<Desktop> = get_desktops()
        .map_err(|e| eyre!("Failed to retrieve virtual desktops: {:?}", e))?;
    let desktop = desktops[(args.index - 1) as usize].clone();
    desktop.set_name(&args.name).map_err(|e| eyre!("Failed to set desktop name: {:?}", e))?;

    Ok(())
}

fn debug_args(args: &SetNameArgs) {
    debug!("SetNameArgs:");
    debug!("  name: {}", args.name);
    debug!("  index: {}", args.index);
}