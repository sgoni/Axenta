﻿namespace Axenta.BuildingBlocks.Email;

public class MailSettings
{
    public string DisplayName { get; set; } = string.Empty;
    public string From { get; set; } = string.Empty;
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool UseStartTls { get; set; }
    public string TemplatePath { get; set; } = string.Empty;
}