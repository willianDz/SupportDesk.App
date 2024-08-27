﻿namespace SupportDesk.App.Models.Auth;

public class VerifyTwoFactorRequest
{
    public string Email { get; set; } = string.Empty!;
    public string TwoFactorCode { get; set; } = string.Empty!;
}
