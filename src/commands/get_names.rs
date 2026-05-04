use color_eyre::eyre::{eyre, Result};
use log::info;

use crate::utils::desktop_utils;

pub fn get_names() -> Result<()> {
    info!("Running the 'get-names' command");

    let desktops = desktop_utils::get_desktops()?;

    for desktop in &desktops {
        let name = desktop.get_name().map_err(|e| eyre!("Failed to get desktop name: {:?}", e))?;
        let index = desktop.get_index().map_err(|e| eyre!("Failed to get desktop index: {:?}", e))?;
        println!("Name of desktop {}: {}", index + 1, name);
    }

    Ok(())
}