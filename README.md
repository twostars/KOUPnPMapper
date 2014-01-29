Knight Online UPnP Mapper
============

Provides simple port forwarding via UPnP for Knight Online.

You can run it either as a standalone app (KOUPnPMapperApp.exe), or as a service (KOUPnPMapperService.exe).
The ports will remain forwarded for as long as either process is running.

Configuration
============
Default ports are set to 15100 (Knight Online login server) & 15001 (Knight Online game server), 
however they can be configured via the "Ports" setting in the EXE's associated config file.

The IP address chosen to forward to is the first IPv4 entry found on your system.
If this is incorrect (it is shown via the standalone app, KOUPnPMapperApp.exe), it can be 
easily overridden via the "LocalIP" setting in the EXE's associated config file.

Service
============
The service (KOUPnPMapperService.exe) can be installed in one of two ways:
 * By simply running it.
 * Command-line: ```KOUPnPMapperService.exe install``` (or -i, or -install)

To uninstall, you should use the command-line:
```KOUPnPMapperService.exe uninstall``` (or -u, or -uninstall)