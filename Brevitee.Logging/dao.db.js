var database = {
    nameSpace: "Brevitee.Logging",
    schemaName: "DaoLogger2",
    xrefs: [
        [ "Event", "Param" ]
    ],
    tables: [
        {
            name: "SourceName",
            cols: [
                { Value: "String" }
            ]
        },
        {
            name: "UserName",
            cols: [
                { Value: "String", Null: false }
            ]
        },
        {
            name: "CategoryName",
            cols: [
                { Value: "String", Null: false }
            ]
        },
        {
            name: "ComputerName",
            cols: [
                { Value: "String", Null: false }
            ]
        },
        {
            name: "Signature",
            cols: [
                { Value: "String", Null: false }
            ]
        },
        {
            name: "Param",
            cols: [
                { Position: "Int" },
                { Value: "String" }
            ]
        },
        {
            name: "Event",
            fks: [
                { SignatureId: "Signature" },
                { ComputerId: "Computer" },
                { CategoryId: "Category" },
                { SourceId: "Source" },
                { UserId: "User" }
            ],
            cols: [
                { Occurred: "DateTime", Null: false },
                { Severity: "Int" },
                { EventId: "Int" }
            ]
        }
    ]
};
