var database = {
    nameSpace: "Brevitee.Analytics.Data",
    schemaName: "Analytics",
    xrefs: [
        ["Url", "Tag"],
        ["Image", "Tag"]
    ],
    tables: [
        {
            name: "Crawler",
            cols: [
                { Name: "String", Null: false},
                { RootUrl: "String", Null: false}
            ]
        },
        {
            name: "Protocol",
            cols: [
                { Value: "String", Null: false }
            ]
        },
        {
            name: "Domain",
            fks: [],
            cols: [
                { Value: "String", Null: false }
            ]
        },
        {
            name: "Port",
            cols: [
                { Value: "Int", Null: false }
            ]
        },
        {
            name: "Protocol",
            cols: [
                { Value: "String", Null: false }
            ]
        },
        {
            name: "Path",
            cols: [
                { Value: "String" }
            ]
        },
        {
            name: "QueryString",
            cols: [
                { Value: "String" }
            ]
        },
        {
            name: "Fragment",
            cols: [
                { Value: "String" }
            ]
        },
        {
            name: "Url",
            fks: [
                { ProtocolId: "Protocol" },
                { DomainId: "Domain" },
                { PortId: "Port" },
                { PathId: "Path" },
                { QueryStringId: "QueryString" },
                { FragmentId: "Fragment" }
            ]
        },
        {
            name: "Tag",
            cols: [
                { Value: "String", Null: false }
            ]
        },
        {
            name: "Image",
            cols: [
                { Date: "DateTime", Null: false }
            ],
            fks: [
                { UrlId: "Url" },
                { CrawlerId: "Crawler" }
            ]
        }
        // --  end crawlers tables 
    ]
}
