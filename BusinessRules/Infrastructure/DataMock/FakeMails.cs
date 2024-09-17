namespace BusinessRules.Infrastructure.DataMock;

using BusinessRules.Infrastructure.Models;
using System.Collections.Generic;

public static class FakeMails
{
    public static List<Mail> SentMails = new List<Mail>();

    public static void Clear()
    {
        SentMails.Clear();
    }
}
