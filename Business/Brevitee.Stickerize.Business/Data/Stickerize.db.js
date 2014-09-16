var database = {
    nameSpace: "Brevitee.Stickerize.Business.Data",
    schemaName: "Stickerize",
    xrefs: [
        ["Stickerizer", "Stickerizee"],
        ["StickerizableList", "Stickerizable"],
        ["SubSection", "Stickerizable"],
    ],
    tables: [
        {
            name: "LoginTime",
            cols: [
                { DateTime: "DateTime" },
                { UserName: "String" }
            ]
        },
        {
            name: "Stickerizer",
            fks: [
                { ImageId: "Image" }
            ],
            cols: [
                { Created: "DateTime", Null: false },
                { Name: "String", Null: false },
                { DisplayName: "String" },
                { UserName: "String" }
            ]
        },
        {
            name: "Stickerizee",
            fks: [
                { ImageId: "Image" }
            ],
            cols: [
                { Created: "DateTime", Null: false },
                { Name: "String", Null: false},
                { DisplayName: "String" },
                { Gender: "String", Null: false },
                { UserName: "String" }
            ]
        },
        {
            name: "Stickerizable",
            fks: [
                { CreatorId: "Stickerizer" }
            ],
            cols: [
                { Created: "DateTime", Null: false },
                { Name: "String", Null: false},
                { Description: "String" },
                { For: "String", Null: false }
            ]
        },
        {
            name: "StickerizableList",
            fks: [
                { CreatorId: "Stickerizer" }
            ],
            cols: [
                { Created: "DateTime", Null: false },
                { Name: "String", Null: false },
                { Description: "String" },
                { Public: "Boolean", Null: false }
            ]
        },
        {
            name: "Sticker",
            fks: [
                { ImageId: "Image" }
            ],
            cols: [
                { Created: "DateTime", Null: false },
                { Name: "String", Null: false },
                { Description: "String" }
            ]
        },
        {
            name: "Stickerization",
            fks:[
                { StickerId: "Sticker" },
                { StickerizerId: "Stickerizer" },
                { StickerizableId: "Stickerizable" },
                { StickerizeeId: "Stickerizee" }                               
            ],
            cols: [
                { Created: "DateTime", Null: false },
                { ForDate: "DateTime", Null: false},
                { UndoneAt: "DateTime" },
                { IsUndone: "Boolean" }
            ]
        },
        {
            name: "Image",
            cols: [
                { Url: "String", Null: false }
            ]
        },
        {
            name: "SubSection",
            fks: [
                { StickerizableListId: "StickerizableList" }
            ],
            cols: [
                { Created: "DateTime", Null: false },
                { Name: "String" }
            ]
        }
    ]
};
