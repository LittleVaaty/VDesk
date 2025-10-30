use windows::Win32::Foundation::{HWND, LPARAM, BOOL};
use windows::Win32::UI::WindowsAndMessaging::{
    EnumWindows, GetWindowTextW, GetWindowTextLengthW, IsWindowVisible, GetWindowThreadProcessId, 
    MoveWindow, GetSystemMetrics, SM_CXMAXIMIZED, SM_CYMAXIMIZED
};
use windows::Win32::System::Diagnostics::ToolHelp::{
    CreateToolhelp32Snapshot, Process32First, Process32Next, PROCESSENTRY32, TH32CS_SNAPPROCESS
};
use std::ffi::OsString;
use std::os::windows::ffi::OsStringExt;
use log::{debug, trace};
use crate::commands::common::HalfSplit;

pub struct FindWindowData {
    pub target_pid: u32,
    pub hwnd: Option<HWND>,
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

pub fn move_half_split(hwnd: HWND, split: Option<HalfSplit>) -> windows::core::Result<()> {
    trace!("Moving window {:?} to half split: {:?}", hwnd, split);
    match split {
        Some(HalfSplit::Left) => {
            unsafe {
                let width = GetSystemMetrics(SM_CXMAXIMIZED) / 2;
                let height = GetSystemMetrics(SM_CYMAXIMIZED);
                let _ = MoveWindow(hwnd, 0, 0, width, height, true);
            }
        }
        Some(HalfSplit::Right) => {
            unsafe {
                let width = GetSystemMetrics(SM_CXMAXIMIZED) / 2;
                let height = GetSystemMetrics(SM_CYMAXIMIZED);
                let _ = MoveWindow(hwnd, width + 1, 0, width, height, true);
            }
        }
        None => {
            // Do nothing
        }
        _ => {
            // Only Left and Right are supported
            return Err(windows::core::Error::from_win32());
        }
    }
    Ok(())
}

pub fn find_main_window(pid: u32) -> Option<HWND> {
    let data = Box::new(FindWindowData {
        target_pid: pid,
        hwnd: None,
    });
    
    unsafe {
        let data_ptr: *mut FindWindowData = Box::leak(data);
        let _ = EnumWindows(Some(enum_windows_proc), LPARAM(data_ptr as isize));
        let data = Box::from_raw(data_ptr);
        data.hwnd
    }
}

extern "system" fn enum_windows_proc(hwnd: HWND, lparam: LPARAM) -> BOOL {
    unsafe {
        let data = &mut *(lparam.0 as *mut FindWindowData);
        if IsWindowVisible(hwnd).as_bool() {
            let length = GetWindowTextLengthW(hwnd);
            if length > 0 {
                let mut window_pid = 0;
                GetWindowThreadProcessId(hwnd, Some(&mut window_pid));
                let mut buffer: Vec<u16> = vec![0; (length + 1) as usize];
                let read_len = GetWindowTextW(hwnd, &mut buffer);
                if read_len > 0 {
                    let title = OsString::from_wide(&buffer[..read_len as usize]);
                    trace!("Window : {:?}, hWnd : {:?} | PID : {}, Target PID : {}", title, hwnd, window_pid, data.target_pid);
                }
                if window_pid == data.target_pid {
                    data.hwnd = Some(hwnd);
                    debug!("Window found with hwnd: {:?}", hwnd);
                    return BOOL(0);
                }
            }
        }
    }
    BOOL(1)
}