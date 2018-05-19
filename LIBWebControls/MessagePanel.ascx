<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessagePanel.ascx.cs" Inherits="LIBWebControls.MessagePanel" %>

<div class="message-container">
    <asp:Panel ID="pnlMessage" Visible="false" runat="server">
        <div class="message centered">
            <asp:Label ID="lblMessage" runat="server" />
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlError" Visible="false" runat="server">
        <div class="error centered">
            <asp:Label ID="lblError" runat="server" />
        </div>
    </asp:Panel>
</div>