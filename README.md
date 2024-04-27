# VDesk

[![.NET](https://github.com/LittleVaaty/VDesk/actions/workflows/dotnet-CI.yml/badge.svg)](https://github.com/LittleVaaty/VDesk/actions/workflows/dotnet-CI.yml)

This is a fork of [eksime/VDesk](https://github.com/eksime/VDesk) that support Windows 10 and 11.
The main reason of the fork is that the original project seam to be abadonned.

> **Note :** due to migration in .netcore, the command argument have change. If you install this version, you will need to addapt your usage of vdesk.


## Install

#### Requirements
- Windows 11 or Windows 10 build 19041 (20H1) and later
- Dotnet/.NET Runtime at least 6 (e.g. download latest Long Term Support Version and there either .NET Runtime or .NET Desktop Runtime 8.0.4 - the first is sufficient for this app - from this page: https://dotnet.microsoft.com/en-us/download/dotnet )

### Manual Install:
- Unistall previous vdesk installation
- Download vdesk from the [releases page](https://github.com/LittleVaaty/VDesk/releases/).
- Unzip the archive and put the exe where you want
- Add the location to your path
> **TODO:** provide more info !!


## Usage:

`vdesk create <number>`

`vdesk run [options] <Command>`

`vdesk move [options] <ProcessName>`

`vdesk switch <Number>`

Run `vdesk [command] --help` for more information about a command.

### Examples:
Create total of 5 desktops:

`vdesk create 5`

Run notepad ***o***n virtual desktop ***n***umber 4 without switching:

`vdesk run -n -o 4 notepad.exe`

Now the same in long syntax form instead of short:

`vdesk run --no-switch --on 4 notepad.exe`


> **Note:** If VDesk doesn't work at first, check the program's command line options for ways to create a new window - For example Chrome has the `/new-window` argument which allows it to function with VDesk.

Move notepad on virtual desktop 5 half split to right

`vdesk move --half-split right -o 5 notepad`

Run a program on desktop n, and prevent switching to it:

`vdesk run --on n --no-switch <command>`

To launch notepad on current desktop:

`vdesk run notepad`

To launch notepad on desktop 3 and open `C:\some file.txt` using it as an ***a***rgument:

`vdesk run -o 3 notepad -a "C:\some file.txt"`

To open Github in a new Chrome window on the second desktop split half screen to the left:

`vdesk run -o 2 --half-split left "C:\Program Files\Google\Chrome\Application\chrome.exe" -a "/new-window https://github.com"`

To launch a new Firefox using several arguments which also contain quotation marks use `\"` for escaping:

`vdesk run --on 2 --no-switch "C:\Program Files\Mozilla Firefox\firefox.exe" -a "-P \"GPT-Nutzung\" -no-remote \"localhost:3000\""`

Note: If you use powershell instead of cmd you cannot use that `\"` notation to handle the quotation in the arguments, use `'` instead in the embracing qutotation marks: 
`vdesk run --on 2 --no-switch "C:\Program Files\Mozilla Firefox\firefox.exe" --arguments '-P "GPT-Nutzung" -no-remote "localhost:3000"'`

## Copyright notice

Copyright (C) 2018

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
