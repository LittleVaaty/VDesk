use color_eyre::eyre::{Result, eyre};
use log::{info};
use winvd::{get_desktops};


pub fn count_virtual_desktops() -> Result<()> {
    info!("Running the 'total' command");

    let desktops = get_desktops()
        .map_err(|e| eyre!("Failed to retrieve virtual desktops: {:?}", e))?;
    
    println!("Number of virtual desktops: {}", desktops.len());
    Ok(())
}