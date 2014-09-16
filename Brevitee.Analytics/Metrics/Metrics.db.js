var database = {
    nameSpace: "Brevitee.Analytics.Metrics",
    schemaName: "Metrics",
    xrefs: [

    ],
    tables: [
        {
            name: "Timer",
            cols: [
                { Value: "String", Null: false }
            ]
        },
        {
            name: "MethodTimer",
            fks:[
                { TimerId: "Timer" }
            ],
            cols: [
                { MethodName: "String", Null: false }
            ]
        },
        {
            name: "LoadTimer",
            fks: [
                { TimerId: "Timer" }
            ],
            cols: [
                { UrlId: "Int" } // maps to crawler.url
            ]
        },
        {
            name: "CustomTimer",
            fks: [
                { TimerId: "Timer" }
            ],
            cols: [
                { Name: "String", Null: false },
                { Description: "String" }
            ]
        },
        {
            name: "Counter", 
            cols: [                
                { Value: "Int", Null: false }
            ]
        },
        {
            name: "MethodCounter",
            fks: [
                { CounterId: "Counter" }
            ],
            cols: [
                { MethodName: "String", Null: false }
            ]
        },
        {
            name: "LoadCounter",
            fks: [
                { CounterId: "Counter" }
            ],
            cols: [
                { UrlId: "Int", Null: false } // maps to crawlers.url
            ]
        },
        {
            name: "UserIdentifier",
            cols: [
                { Id: "String", Null: false },
                { Name: "String", Null: false }
            ]
        },
        {
            name: "ClickCounter",
            fks: [
                { CounterId: "Counter" },
                { UserIdentifierId: "UserIdentifier" }
            ],
            cols: [
                { UrlId: "Int", Null: false } // maps to crawlers.url
            ]
        },
        {
            name: "LoginCounter",
            fks: [
                { CounterId: "Counter" },
                { UserIdentifierId: "UserIdentifier" }
            ]
        }
        // -- end classification tables
    ]
}
