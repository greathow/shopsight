﻿namespace PaperID.Models
{
    using System;

    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}
