use clap::Args;
use color_eyre::eyre::{eyre, Result};
use winvd::{get_desktop_count, create_desktop};
use log::{debug, info};

#[derive(Args, Debug)]
pub struct CreateArgs {
    pub number: u32,
}

pub fn create_virtual_desktops(args: CreateArgs) -> Result<()> {
    info!("Running the 'create' command");

    let current_count = get_desktop_count()
        .map_err(|e| eyre!("Failed to get desktop count: {:?}", e))?;

    let desktops_to_create = args.number.saturating_sub(current_count);
    debug!("Creating {} virtual desktops", desktops_to_create);

    for _ in 0..desktops_to_create {
        create_desktop()
            .map_err(|e| eyre!("Failed to create desktop: {:?}", e))?;
    }

    Ok(())
}