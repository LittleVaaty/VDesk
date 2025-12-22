use color_eyre::eyre::{Result, eyre};
use log::debug;
use winvd::{Desktop};
use windows::Win32::Foundation::HWND;

pub fn get_desktops() -> Result<Vec<Desktop>> {
    winvd::get_desktops().map_err(|e| eyre!("Failed to retrieve virtual desktops: {:?}", e))
}

pub fn switch_desktop(desktop: &Desktop) -> Result<()> {
    winvd::switch_desktop(*desktop).map_err(|e| eyre!("Failed to switch to virtual desktop: {:?}", e))
}

pub fn move_window_to_desktop(desktop: &Desktop, hwnd: &HWND) -> Result<()> {
    winvd::move_window_to_desktop(*desktop, hwnd)
    .map_err(|e| eyre!("Failed to move window to virtual desktop: {:?}", e))
}

pub fn get_desktop_id_by_name_or_index(index_or_name: &str) -> Result<Option<Desktop>> {
    // Essayer de parser index_or_name en usize
    debug!("Recherche du bureau virtuel par index: {}", index_or_name);
    let desktops = winvd::get_desktops().map_err(|e| eyre!("Failed to retrieve virtual desktops: {:?}", e))?;
    if let Ok(virtual_desktop_id) = index_or_name.parse::<usize>() {
        if virtual_desktop_id > 0 && virtual_desktop_id <= desktops.len() {
            return Ok(Some(desktops[virtual_desktop_id - 1].clone()));
        } else {
            return Ok(None);
        }
    }
    
    // Recherche par nom
    debug!("Recherche du bureau virtuel par nom: {}", index_or_name);
    for (_, desktop) in desktops.iter().enumerate() {
        let name = desktop.get_name().expect("Cannot get desktop name");
        
        if name == *index_or_name {
            return Ok(Some(desktop.clone()));
        }
    }
    
    Ok(None)
}