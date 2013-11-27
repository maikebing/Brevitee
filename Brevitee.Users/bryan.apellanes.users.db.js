var database = {
    nameSpace: "Brevitee.Users.Data",
    schemaName: "Users",
    xrefs: [
        ["User", "Role"]
    ],
    tables: [
        {
            name: "User",
            cols:[
                { CreationDate: "DateTime", Null: false },
                { IsDeleted: "Boolean" },
                { UserName: "String", Null: false },
                { IsApproved: "Boolean"},
                { Email: "String", Null: true }
            ]
        },
        {
            name: "Account",
            fks:[
                { UserId: "User" }
            ],
            cols:[
                { Token: "String", Null: false },
                { Provider: "String", Null: false },
                { ProviderUserId: "String", Null: false },
                { Comment: "String" },
                { CreationDate: "DateTime", Null: false },
                { IsConfirmed: "Boolean" },
                { ConfirmationDate: "DateTime" }
            ]
        },
        {
            name: "Password",
            fks:[
                { UserId: "User" }
            ],
            cols:[
                { Value: "String", Null: false },
                { Salt: "String", Null: false },
                { Sha1: "String", Null: false }
            ]
        },
        {
            name: "PasswordReset",
            fks:[
                { UserId: "User" }
            ],
            cols:[
                { Token: "String", Null: false },
                { DateTime: "DateTime", Null: false },
                { WasReset: "Boolean" },
                { ExpiresInMinutes: "Int", Null: false }
            ]
        },
        {
            name: "PasswordFailure",
            fks:[
                { UserId: "User" }
            ],
            cols:[
                { DateTime: "DateTime" }
            ]
        },
        {
            name: "LockOut",
            fks:[
                { UserId: "User" }
            ],
            cols: [
                { DateTime: "DateTime" },
                { Unlocked: "Boolean"},
                { UnlockedDate: "DateTime" }
            ]
        },
        {
            name: "Login",
            fks:[
                { UserId: "User" }
            ],
            cols:[
                { DateTime: "DateTime" }
            ]
        },
        {
            name: "PasswordQuestion",
            fks:[
                { UserId: "User" }
            ],
            cols:[
                { Value: "String", Null: false },
                { Answer: "String", Null: false }
            ]
        },
        {
            name: "Setting",
            fks:[
                { UserId: "User" }
            ],
            cols:[
                { Key: "String", Null: false },
                { ValueType: "String", Null: false },
                { Value: "Byte", Null: false }
            ]
        },
        {
            name: "Session",
            fks:[
                { UserId: "User" }
            ],
            cols:[
                { Identifier: "String", Null: false },
                { CreationDate: "DateTime" },
                { LastActivity: "DateTime" },
                { IsActive: "Boolean" }
            ]
        },
        {
            name: "SessionState",
            fks:[
                { SessionId: "Session" }
            ],
            cols:[
                { Name: "String" },
                { ValueType: "String" },
                { Value: "Byte" }
            ]
        },
        {
            name: "UserBehavior",
            fks:[
                { SessionId: "Session" }
            ],
            cols:[
                { DateTime: "DateTime", Null: false },
                { Selector: "String"}, // a css/jquery selector identifying the source of the event
                { EventName: "String" },
                { Data: "Byte"},
                { Url: "String" }
            ]
        },
        {
            name: "Role",
            cols:[
                { Name: "String", Null: false, MaxLength: "255" }
            ]
        }
    ]
}
