﻿using System;
using System.Collections.Generic;

namespace TVQE.Context;

public partial class UsersUserPermission
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public int PermissionId { get; set; }

    public virtual AuthPermission Permission { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
