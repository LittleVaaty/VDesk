use clap::Args;
use color_eyre::eyre::Result;
use winvd::{get_desktop_count, create_desktop};
use log::{debug, info};

#[derive(Args, Debug)]
pub struct CreateArgs {
    pub number: u32,
}

pub fn create_virtual_desktops(args: CreateArgs) -> Result<()> {
    info!("Running the 'create' command");

    let desktop_ids = get_desktop_count().expect("cannot get desktip ids count");

    let mut number = args.number;
    debug!("Create {0} virtual desktop", desktop_ids - number);

    while number > desktop_ids {
        let _ = create_desktop();
        number += 1;
    }

    Ok(())
}