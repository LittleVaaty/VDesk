start VDesk.exe create -v 4
start VDesk.exe set-name -o 2 test-vdesk
start VDesk.exe run -o 1 --half-split right notepad -a "/A sample.bat"
start VDesk.exe switch 3
timeout /t 1
start VDesk.exe move -o 2 -n notepad