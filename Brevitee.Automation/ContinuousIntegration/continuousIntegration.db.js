var database = {
    nameSpace: "Brevitee.Automation.ContinuousIntegration.Data",
    schemaName: "ContinuousIntegration",
    xrefs: [
       // ["StickerizableList", "Stickerizable"],       
    ],
    tables: [
        {
            name: "BuildJob",
            cols: [
                { Name: "String", Null: false },
                { UserName: "String", Null: false },
                { HostName: "String", Null: false },
                { OutputPath: "String", Null: false }
            ]
        },
        {
            name: "BuildResult",
            fks: [
               { BuildJobId: "BuildJob" }
            ],
            cols: [
                { Success: "Boolean", Null: false },
                { Message: "String" }                
            ]
        }
    ]
};
