var database = {
    nameSpace: "Brevitee.Analytics.Data",
    schemaName: "Analytics",
    xrefs: [
        ["Url", "Tag"],
        ["Image", "Tag"],
        ["HtmlElement", "Style"],
        ["HtmlElement", "Attribute"]
    ],
    tables: [
        // -- history
        {
            name: "WriteHistory",
            cols: [
                { TableName: "String", Null: false },
                { ColumnName: "String", Null: false },
                { DateTime: "DateTime", Null: false },
                { Value: "Byte", Null: true },
                { ValueType: "String", Null: false }
            ]
        },
        // -- end history
        {
            name: "UserIdentifier",
            cols:[
                { UserName: "String", Null: false },
                { Source: "String", Null: false }
            ]
        },
        {
            name: "ImageRating",
            fks: [
                { ImageId: "Image" },
                { UserIdentifierId: "UserIdentifier" }
            ],
            cols:[
                { Value: "Int" }
            ]
        },
        {
            name: "ImageCrawler",
            cols: [
                { Name: "String", Null: false},
                { RootUrl: "String", Null: false}
            ]
        },
        {
            name: "HtmlElement",
            fks: [
                { UrlId: "Url" },
                { ContentId: "Content" }
            ],
            cols: [
                { DomId: "String", Null: false },
                { TagName: "String", Null: false }
            ]
        },
        {
            name: "Content",
            cols: [
                { Hash: "String", Null: true },
                { HashAlgorithm: "String", Null: true },
                { Date: "DateTime", Null: false },
                { Value: "String", Null: false }
            ]
        },
        {
            name: "Style",
            cols: [
                { Name: "String", Null: false },
                { Value: "String", Null: true }
            ]
        },
        {
            name: "Attribute",
            cols: [
                { Name: "String", Null: false },
                { Value: "String", Null: true }
            ]
        },
        // -- classification tables
        {
            name: "Category",
            cols: [
                { Value: "String", Null: false },
                { DocumentCount: "Long", Null: false }
            ]
        },
        {
            name: "Feature",
            fks: [
                { CategoryId: "Category" }
            ],
            cols: [
                { Value: "String", Null: false },
                { FeatureToCategoryCount: "Long", Null: false }
            ]
        }
        // -- end classification tables
        ,
        // -- crawlers tables
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
