use clap::Args;
use color_eyre::eyre::{Result, eyre, Context};
use log::{debug, info};

use crate::{
    commands::common::HalfSplit,
    utils::{desktop_utils, window_utils, process_utils},
};

/// Arguments pour la commande `move`
#[derive(Args, Debug)]
pub struct MoveWindowArgs {
    /// Bureau virtuel cible (nom ou index)
    #[arg(short = 'o', long = "on", required = true)]
    pub index_or_name: String,

    /// Nom du processus ou titre de la fenêtre à déplacer
    pub process: String,

    /// Ne pas basculer vers le bureau après le déplacement
    #[arg(short = 'n', long = "no-switch")]
    pub no_switch: bool,

    /// Positionner la fenêtre sur une moitié de l'écran
    #[arg(long = "half-split", value_enum)]
    pub half_split: Option<HalfSplit>,
}

pub fn move_window(args: MoveWindowArgs) -> Result<()> {
    info!("Running the 'move' command");
    debug!("MoveWindowArgs: {:?}", args);

    let desktop = desktop_utils::get_desktop_id_by_name_or_index(&args.index_or_name)?
        .ok_or_else(|| eyre!("Virtual desktop not found: {}", &args.index_or_name))?;
    
    let pid = process_utils::find_process_by_name(&args.process)
        .ok_or_else(|| eyre!("Failed to find process: {}", &args.process))?;

    let hwnd = window_utils::find_main_window(pid)
        .ok_or_else(|| eyre!("Failed to find main window for PID {}", pid))?;

    desktop_utils::move_window_to_desktop(&desktop, &hwnd)?;

    window_utils::move_half_split(hwnd, args.half_split)
        .context("Failed to move the window to the specified half of the screen")?;
    
    if !args.no_switch {
        desktop_utils::switch_desktop(&desktop)?;
    }

    Ok(())
}