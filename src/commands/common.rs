use clap::{ValueEnum};


#[derive(Copy, Clone, Debug, ValueEnum)]
pub enum HalfSplit {
    Left,
    Right,
    Top,
    Bottom,
}