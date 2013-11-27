var database = {
    nameSpace: "Brevitee.Keymaster.Data",
    schemaName: "Keymaster",
    
    tables: [
        {
            name: "Application",
            cols: [
                { Name: "String", Null: false }
            ]
        },
        {
            name: "ApiKey",
            cols: [
                { Private: "String", Null: false },
                { Public: "String", Null: false },
                { CreatedBy: "String", Null: false},
                { Expires: "DateTime", Null: false }
            ]
        }
    ]
};
