use clap::Subcommand;
use color_eyre::eyre::Result;

mod create;
mod total;
mod get_names;
mod get_name;
mod set_name;
mod run;
mod move_window;
mod switch;
pub mod common;

#[derive(Subcommand, Debug)]
pub enum Commands {
    Create(create::CreateArgs),
    Total,
    SetName(set_name::SetNameArgs),
    GetName(get_name::GetNameArgs),
    GetNames,
    Run(run::RunArgs),
    Move(move_window::MoveWindowArgs),
    Switch(switch::SwitchArgs),
}

pub fn handle_command(command: Commands) -> Result<()> {
    match command {
        Commands::Create(args) => create::create_virtual_desktops(args),
        Commands::Total => total::count_virtual_desktops(),
        Commands::SetName(args) => set_name::set_name(args),
        Commands::GetName(args) => get_name::get_name(args),
        Commands::GetNames => get_names::get_names(),
        Commands::Run(args) => run::run(args),
        Commands::Move(args) => move_window::move_window(args),
        Commands::Switch(args) => switch::switch(args),
    }
}