var database = {
    nameSpace: "Brevitee.Crawlers.Data",
    schemaName: "Crawlers",
    tables: [
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
                { Value: "String", Null: false }
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
            name: "Url",
            fks: [
                { ProtocolId: "Protocol" },
                { DomainId: "Domain" },
                { PortId: "Port" },
                { PathId: "Path" },
                { QueryStringId: "QueryString" }
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
            fks: [
                { UrlId: "Url" }
            ]
        }
    ],
    xrefs: [
        ["Url", "Tag"],
        ["Image", "Tag"]
    ]
}
