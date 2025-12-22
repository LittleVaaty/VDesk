use clap::Args;
use color_eyre::eyre::{Result, eyre};
use log::{debug, info};

use crate::utils::desktop_utils;

#[derive(Args, Debug)]
pub struct SwitchArgs {
    /// Bureau virtuel cible (nom ou index)
    pub index_or_name: String,
}

pub fn switch(args: SwitchArgs) -> Result<()> {
    info!("Running the 'switch' command");
    debug_args(&args);

    let desktop = desktop_utils::get_desktop_id_by_name_or_index(&args.index_or_name)?
        .ok_or_else(|| eyre!("Virtual desktop not found: {}", &args.index_or_name))?;

    desktop_utils::switch_desktop(&desktop)
}

fn debug_args(args: &SwitchArgs) {
    debug!("SwitchArgs:");
    debug!("  index_or_name: {}", args.index_or_name);
}

