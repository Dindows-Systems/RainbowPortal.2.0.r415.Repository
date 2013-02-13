
This layout is only intended to work with the zen-hmenu-vsubmenu theme.

You can change the portallogo using the site settings site logo upload.

You can and should disable the IE7Scripts for this theme as the menu
doesn't need it. You do this by changing the web.config appsettings setting for
IE7Script to "", like:
    <add key="Ie7Script" value="" />
This will save you aprox. 25kb per request.

Layout is based on zen-example1 and was created by Yannick Smits (info@goyaweb.nl)