mod commands;
mod utils;

use clap::{Parser};
use commands::Commands;
use color_eyre::eyre::Result;
use log::{info, debug};

#[derive(Parser)]
#[command(name = "vdesk")]
#[command(version)]
#[command(about = "Virtual desktop manager", long_about = None)]
struct Cli {
    /// Verbosity level (-v, -vv, -vvv)
    #[arg(short, long, action = clap::ArgAction::Count)]
    verbose: u8,

    #[command(subcommand)]
    command: Commands,
}

fn init_logger(verbose: u8) {

    let level = match verbose {
        0 => log::LevelFilter::Warn,
        1 => log::LevelFilter::Info,
        2 => log::LevelFilter::Debug,
        _ => log::LevelFilter::Trace,
    };

    env_logger::builder()
    .format_timestamp(None)
    .filter_level(level)
    .init();
}

fn main() -> Result<()> {
    color_eyre::install()?;

    let cli = Cli::parse();

    init_logger(cli.verbose);
    info!("Starting the application");
    if cli.verbose > 0 {
        debug!("Verbosity level: {}", cli.verbose);
    }

    commands::handle_command(cli.command)
}