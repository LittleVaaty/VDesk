use std::process::{Command, Stdio};
use windows::Win32::System::Diagnostics::ToolHelp::{
    CreateToolhelp32Snapshot, Process32First, Process32Next, PROCESSENTRY32, TH32CS_SNAPPROCESS
};
use color_eyre::eyre::{Result, eyre};
use log::{debug, trace};


pub fn launch(exe: &str, args: Option<&str>) -> Result<u32> {
    let mut cmd = Command::new(exe);
    if let Some(a) = args {
        cmd.arg(a);
    }
    let child = cmd.stdin(Stdio::null())
    .stdout(Stdio::null())
    .stderr(Stdio::null())
    .spawn()
    .map_err(|e| eyre!("Failed to launch process: {:?}", e))?;
    
    trace!("Process started with PID {}", child.id());
    Ok(child.id())
}

pub fn find_process_by_name(process_name: &str) -> Option<u32> {
    let mut pid:Option<u32> = None;

    unsafe {
        let snapshot = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0).expect("Failed to create snapshot");
        let mut entry = PROCESSENTRY32 {
            dwSize: std::mem::size_of::<PROCESSENTRY32>() as u32,
            ..Default::default()
        };

        if !Process32First(snapshot, &mut entry).is_err() {
            loop {
                let exe_name = String::from_utf16_lossy(
                                    &entry.szExeFile
                                        .iter()
                                        .take_while(|&&c| c != 0)
                                        .map(|&c| c as u16)
                                        .collect::<Vec<u16>>()
                                );
                trace!("Checking process {} with PID {}", exe_name, entry.th32ProcessID);
                if exe_name.eq_ignore_ascii_case(process_name) {
                    debug!("Found process {} with PID {}", exe_name, entry.th32ProcessID);
                    pid = Some(entry.th32ProcessID);
                    break;
                }

                if Process32Next(snapshot, &mut entry).is_err() {
                    break;
                }
            }
        }
    }
    pid
}