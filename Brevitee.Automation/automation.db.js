var database = {
    nameSpace: "Brevitee.Automation.Data",
    schemaName: "Automation",
    xrefs: [
       // ["StickerizableList", "Stickerizable"],       
    ],
    tables: [
        {
            name: "DeferredJob",
            cols: [
                { Name: "String", Null: false },
                { JobDirectory: "String", Null: false },
                { HostName: "String", Null: false },
                { LastStepNumber: "String", Null: false}
            ]
        },
        {
            name: "RunningJob",
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
