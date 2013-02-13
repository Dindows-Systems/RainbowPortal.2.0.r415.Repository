<%@ page autoeventwireup="false" inherits="Rainbow.Ftb.Imagegallery" language="c#"
    src="ftb.imagegallery.aspx.cs" validaterequest="false" %>

<%@ register assembly="FreeTextBox" namespace="FreeTextBoxControls" tagprefix="FTB" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Image Gallery</title>
</head>
<body>
    <form id="Form1" runat="server" enctype="multipart/form-data">
        <ftb:imagegallery id="ImageGallery1" runat="Server">
        </ftb:imagegallery>
    </form>
</body>
</html>
