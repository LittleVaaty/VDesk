use clap::{Args};
use color_eyre::eyre::{Result, eyre};
use log::{debug, info, trace};
use winvd::{get_desktops, move_window_to_desktop, switch_desktop, Desktop};

use crate::{
    commands::common::{self, HalfSplit},
    utils::window_utils::{find_main_window, find_process_by_name, move_half_split},
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

    /// Positionner la fenêtre sur une moitié de l’écran
    #[arg(long = "half-split", value_enum)]
    pub half_split: Option<HalfSplit>,
}

pub fn move_window(args: MoveWindowArgs) -> Result<()> {
    info!("Running the 'move' command");
    debug_args(&args);

    let desktops: Vec<Desktop> = get_desktops()
        .map_err(|e| eyre!("Failed to retrieve virtual desktops: {:?}", e))?;

    let desktop: Desktop = common::get_desktop_id_by_name_or_index(desktops, &args.index_or_name)
        .ok_or_else(|| eyre!("Virtual desktop not found: {}", &args.index_or_name))?;
    
    let pid = find_process_by_name(&args.process)
        .ok_or_else(|| eyre!("Failed to find process by name: {}", &args.process))?;

    let hwnd = find_main_window(pid).ok_or_else(|| eyre!("Failed to find main window for PID {}", pid))?;
    trace!("hwnd: {:?}", hwnd);

    move_window_to_desktop(desktop, &hwnd)
        .map_err(|e| eyre!("Failed to move the window to the virtual desktop: {:?}", e))?;

    move_half_split(hwnd, args.half_split)
        .map_err(|e| eyre!("Failed to move the window to the specified half of the screen: {:?}", e))?;

    if args.no_switch {
        return Ok(());
    }

    switch_desktop(desktop)
        .map_err(|e| eyre!("Failed to switch to virtual desktop: {:?}", e))?;

    Ok(())
}

fn debug_args(args: &MoveWindowArgs) {
    debug!("MoveWindowArgs:");
    debug!("  index_or_name: {}", args.index_or_name);
    debug!("  process: {}", args.process);
    debug!("  no_switch: {}", args.no_switch);
    debug!("  half_split: {:?}", args.half_split);
}