var database = {
    nameSpace: "Brevitee.Automation.Build.Data",
    schemaName: "Stickerize",
    xrefs: [
       // ["StickerizableList", "Stickerizable"],       
    ],
    tables: [
        //{
        //    name: "Stickerizee",
        //    fks: [
        //        { StickerizerId: "Stickerizer" },
        //        { ImageId: "Image" }
        //    ],
        //    cols: [
        //        { Name: "String", Null: false },
        //        { DisplayName: "String" },
        //        { Gender: "String", Null: false },
        //        { UserName: "String" }
        //    ]
        //}
        {
            name: "BuildJob",
            cols: [
                { Name: "String", Null: false },
                { UserName: "String", Null: false },
                { HostName: "String", Null: false },
                { OutputPath: "String", Null: false }
            ]
        }
    ]
};
