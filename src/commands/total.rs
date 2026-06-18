use color_eyre::eyre::{Result, Context};
use log::info;

use crate::utils::desktop_utils;


pub fn count_virtual_desktops() -> Result<()> {
    info!("Running the 'total' command");

    let desktops = desktop_utils::get_desktops()
        .context("Failed to retrieve virtual desktops")?;
    
    println!("Number of virtual desktops: {}", desktops.len());
    Ok(())
}