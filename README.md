# Guild Wars 2 plug-in for Playnite

Maintaining multiple accounts for Guild Wars 2 is a hassle.
Every time you want to log in to a different account, you have to type in the credentials of that account.
There are multiple solutions to this, but this approach integrates it into an open source game launcher with support for other games besides Guild Wars 2, [Playnite](https://playnite.link/).

Once this plug-in is installed in Playnite, you can configure as many accounts as you want.
Credentials aren't saved in this plug-in nor in Playnite.
The trick is explained further below in the technical details.

## Installing
Grab the zip archive from the latest release (or compile it yourself).
Extract the folder into `<PLAYNITE_INSTALLATION_FOLDER>/Extensions` or into `%APPDATA%/Playnite/Extensions`.
Restart Playnite and the plug-in should be available for use.

## Usage
Before you can use the plug-in, it needs to be configured in the Playnite settings.
Once the settings window is opened, open the Libraries tab.
Guild Wars 2 should be listed there, if not, something went wrong with the installation.

In here, the path to the Guild Wars 2 executable should be defined.
If you want, you can also define the command line arguments that will be passed to Guild Wars 2.
After that, it's just a matter of adding some accounts.
Every account needs a unique ID and a name.
The custom path and arguments are both optional and can be used to override the default settings for just that account.

After pressing Save, the accounts will show up (or be removed if you deleted some).
They will be named as *Guild Wars 2 - NAME*.
Now you can use them to launch the game with your accounts.

**Do note however, that every time when you launch a new account for the first time, requires you to set it up through the Guild Wars 2 launcher.**
As mentioned before, this plug-in does not store any Guild Wars 2 credentials and has therefore no knowledge about the actual accounts you use.

## Technical details
The trick this plug-in uses is not that exciting at all, but very useful in this case.
Guild Wars 2 maintains a special file in the AppData folder called `Local.dat`.
This is the file where all your account settings are saved, including your credentials (encrypted of course).
This plug-in just swaps this file in and out based on the account you launch.
That's also where the unique ID comes in.
When you're not using the account, its ID is appended to `Local.dat` to store the file without having it overwritten accidentally.
All these files are saved in the same folder as the original `Local.dat` file.

## Compiling
This project uses Visual Studio 2017.
Compiling it should be as easy as hitting *Build*.

## Disclaimer
This is an **unofficial** library.
The author of this library is not associated with ArenaNet nor with any of its partners.
Modifying Guild Wars 2 through any third party software is not supported by ArenaNet nor by any of its partners.
By using this software, you agree that it is at your own risk and that you assume all responsibility.
There is no warranty for using this software.

If you do not agree with this, please do not use this software.
