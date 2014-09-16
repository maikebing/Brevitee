var database = {
    nameSpace: "Brevitee.Distributed.Files",
    schemaName: "DistributedFiles",
    xrefs: [        
    ],
    tables: [
        {
            name: "Node",
            fks: [
                { ParentId: "Node" }
            ],
            cols: [
                { Name: "String" },
                { IsFolder: "Boolean" }
            ]
        },
        {
            name: "File",
            fks: [
                { NodeId: "Node" },
                { StartChunkId: "Chunk" },
                { Size: "Int" }
            ]
        },
        {
            name: "Chunk",
            fks: [
                { PreviousChunkId: "Chunk" },
                { NextChunkId: "Chunk" },
                { Size: "Int", Null: false }
            ],
            cols: [
                { Data: "Byte" },                
            ]
        }
    ]
};
