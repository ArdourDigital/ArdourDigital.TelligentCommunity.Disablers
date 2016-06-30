# Disablers for Telligent Community - EXPERIMENTAL

This library provides functionality to allow standard features to be disabled for certain code scopes when working with Telligent Community.

## Supported Versions
This is an experimental library, and should be used with care. It should work with any modern version of Telligent Community but has not been tested on all versions.

If you have a problem please raise an issue in Github, or even better create a Pull Request! 

## Installation

You can install the library by [downloading and referencing the latest version](https://github.com/ArdourDigital/ArdourDigital.TelligentCommunity.Disablers/releases/latest), or by using nuget:

```
Install-Package ArdourDigital.TelligentCommunity.Disablers
```
See the individual features below for more information on usage, and installation.

## Features

### Disable Notifications for code scope
This works when using the In-Process API to make calls that generate notifications that you want to stop for this code. This can be useful when doing housekeeping tasks, such as moving users into groups etc. and not wanting to send emails or live notifictaions.

You must enable the plugin for this to work, by logging in as an administrator, and going to `Administration` > `Extensions` and finding `Ardour Digital - Notification Disabler`, check the `Enabled` check box, and `Save`.

You should then wrap any code blocks you want to disable notifications for with a using statement, creating a new `NotificationDisabler`, for example the following code would normally create a notification telling the user they have been added to a group:

```
PublicApi.GroupUserMembers.Create(groupId, userId);
```
By using the `NotificationDisabler` the notification will not be sent:
 
```
using (var disabler = new ArdourDigital.TelligentCommunity.Disablers.Notifications.NotificationDisabler())
{
    PublicApi.GroupUserMembers.Create(groupId, userId);
}
```
#### NOTES
1. In 9.x the `Process` methods have been added, I haven't tried these yet, but it appears the same thing can be achieved using:
```
PublicApi.Process.RunProcessWithDisabledNotifications(() =>
{
    PublicApi.GroupUserMembers.Create(groupId, userId);
});
```
2. Some emails aren't generated as notifications - for example thread movement. The best way to deal with that is to temporarily disable the plugins responsible for the emails (in this case `Forum Thread Moved Email Plugin` and `Forum Thread Moved To Private Group Email Plugin`.