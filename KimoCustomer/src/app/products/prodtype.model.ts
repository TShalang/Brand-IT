export class Prodtypes {
    Product_Type_ID: number;
    Product_Category_ID: number;  
    Template_ID: number; 
    Name: string;
    ImageString: string;
}

export class DesignImage {
    Product_Type_ID: number;
    ImageString: string;
}

export class Designs {
    Product_Type_ID: number;
    Design_ID: number;  
    Template_ID: number; 
    Customer_ID: number; 
    Design_Name: string;
}

export class DesignLine {
    Position: number;
    JobType: string;  
    Text: string; 
    Width: number; 
    Height: string;
    Additional_Instructions: string;
}