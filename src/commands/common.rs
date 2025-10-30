use clap::{ValueEnum};
use log::debug;


#[derive(Copy, Clone, Debug, ValueEnum)]
pub enum HalfSplit {
    Left,
    Right,
    Top,
    Bottom,
}

pub fn get_desktop_id_by_name_or_index(desktops: Vec<winvd::Desktop>, index_or_name: &String,) -> Option<winvd::Desktop> {
    // Essayer de parser index_or_name en usize
    debug!("Recherche du bureau virtuel par index: {}", index_or_name);
    if let Ok(virtual_desktop_id) = index_or_name.parse::<usize>() {
        if virtual_desktop_id > 0 && virtual_desktop_id <= desktops.len() {
            return Some(desktops[virtual_desktop_id - 1].clone());
        } else {
            return None;
        }
    }

    // Recherche par nom
    debug!("Recherche du bureau virtuel par nom: {}", index_or_name);
    for (_, desktop) in desktops.iter().enumerate() {
        let name = desktop.get_name().expect("Cannot get desktop name");

        if name == *index_or_name {
            return Some(desktop.clone());
        }
    }

    None
}