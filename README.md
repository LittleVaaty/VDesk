# VDesk

[![.NET](https://github.com/LittleVaaty/VDesk/actions/workflows/dotnet-CI.yml/badge.svg)](https://github.com/LittleVaaty/VDesk/actions/workflows/dotnet-CI.yml)

VDesk is a fork of [eksime/VDesk](https://github.com/eksime/VDesk) that supports Windows 10 and 11.  
This fork was created because the original project appears to be abandoned.

> **Note:** Due to the migration to .NET Core, the command-line arguments have changed. If you install this version, you will need to adapt your usage of VDesk.

---

## 📥 Installation

### Prerequisites
- **Operating System**: Windows 11 or Windows 10 build 19041 (20H1) or later.
- **.NET Runtime**: At least version 8. You can download the latest Long Term Support (LTS) version from [this page](https://dotnet.microsoft.com/en-us/download/dotnet).

### Manual Installation
1. Uninstall any previous version of VDesk.
2. Download the latest version of VDesk from the [releases page](https://github.com/LittleVaaty/VDesk/releases/).
3. Extract the archive and place the executable (`vdesk.exe`) in a location of your choice.
4. Add this location to your `PATH` environment variable.

---

## 📦 Features

- Create multiple virtual desktops.
- Switch between desktops.
- Launch applications on a specific desktop.
- Move processes to another desktop.
- Position windows on the left or right half of the screen.
- Prevent automatic switching when launching applications.
- Pass complex arguments via the command line.
- Add a delay for applications that are slow to start.

---

## 🧰 Usage

### General Syntax
```sh
vdesk [command] [options]
```

### Available Commands
| Command            | Description                                      |
|--------------------|--------------------------------------------------|
| `create <Number>`  | Creates one or more virtual desktops.            |
| `total`            | Displays the total number of virtual desktops.   |
| `set-name <name>`  | Sets the name of a virtual desktop.              |
| `get-name`         | Displays the name of a specific virtual desktop. |
| `get-names`        | Displays the names of all virtual desktops.      |
| `run <command>`    | Runs a command on a virtual desktop.             |
| `move <command>`   | Moves an open application to a virtual desktop.  |
| `switch <index>`   | Switches to a specific virtual desktop.          |

### Global Options
| Option             | Description                                      |
|--------------------|--------------------------------------------------|
| `-?`, `-h`, `--help` | Displays help and usage information.            |
| `--version`        | Displays version information.                    |
| `--info`           | Displays additional information.                 |

---

## 🚀 Examples

### Create Virtual Desktops
Create a total of 5 virtual desktops:
```sh
vdesk create 5
```

### Launch an Application on a Specific Desktop
Launch Notepad on virtual desktop number 4 without switching:
```sh
vdesk run --no-switch --on 4 notepad.exe
```

### Move an Application to Another Desktop
Move Notepad to virtual desktop 5 and position it on the right half of the screen:
```sh
vdesk move --half-split right --on 5 notepad
```

### Run a Command with Complex Arguments
Launch Firefox with arguments containing quotes:
```sh
vdesk run --on 2 --no-switch "C:\Program Files\Mozilla Firefox\firefox.exe" -a "-P \"Profile\" -no-remote \"localhost:3000\""
```

### Add a Delay for Slow Applications
Launch Outlook with a 1000 ms delay before moving it:
```sh
vdesk run --on 2 --no-switch -w 1000 "C:\Program Files\Microsoft Office\root\Office16\OUTLOOK.EXE"
```

---

## 🤝 Contributing

Contributions are welcome!

While new features may be added slowly due to limited time, pull requests and bug reports are encouraged.

---

## 📜 License

This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful, but **WITHOUT ANY WARRANTY**; without even the implied warranty of **MERCHANTABILITY** or **FITNESS FOR A PARTICULAR PURPOSE**. See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with this program. If not, see [http://www.gnu.org/licenses/](http://www.gnu.org/licenses/).