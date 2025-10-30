use clap::Args;
use color_eyre::eyre::{Result, eyre};
use log::{debug, info};
use winvd::{switch_desktop, get_desktops, Desktop};

use crate::commands::common;


#[derive(Args, Debug)]
pub struct SwitchArgs {
    /// Bureau virtuel cible (nom ou index)
    pub index_or_name: String,
}

pub fn switch(args: SwitchArgs) -> Result<()> {
    info!("Running the 'switch' command");
    debug_args(&args);
    
    let desktops: Vec<Desktop> = get_desktops()
        .map_err(|e| eyre!("Impossible de récupérer les bureaux virtuels: {:?}", e))?;

    let desktop: Desktop = common::get_desktop_id_by_name_or_index(desktops, &args.index_or_name)
        .ok_or_else(|| eyre!("Bureau virtuel non trouvé: {}", &args.index_or_name))?;

    switch_desktop(desktop)
        .map_err(|e| eyre!("Impossible de basculer sur le bureau virtuel: {:?}", e))?;

    Ok(())
}

fn debug_args(args: &SwitchArgs) {
    debug!("SwitchArgs:");
    debug!("  index_or_name: {}", args.index_or_name);
}

