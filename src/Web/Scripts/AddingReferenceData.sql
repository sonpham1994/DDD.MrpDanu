USE [DDD.MrpDanu]

INSERT MaterialType
VALUES (1, 'Material'),
        (2, 'Subassemblies');

INSERT RegionalMarket
VALUES (1, 'None', 'None'),
        (2, 'NewYork', 'New York city'),
        (3, 'CN', 'China'),
        (4, 'NA', 'North America'),
        (5, 'EU', 'Europe'),
        (6, 'KR', 'Korean'),
        (7, 'UK', 'the United Kingdom'),
        (8, 'Florida', 'Florida');

INSERT CurrencyType
VALUES (1, 'USD'),
        (2, 'SGD'),
        (3, 'VND');

INSERT LocationType
VALUES (1, 'Oversea'),
        (2, 'Domestic');

INSERT TransactionalPartnerType
VALUES (1, 'Customer'),
        (2, 'Supplier'),
        (3, 'Both');
    
INSERT Country
VALUES (1, 'VN', 'Vietnam'),
        (2, 'US', 'the United States'),
        (3, 'UK', 'the United Kingdom'),
        (4, 'CN', 'China'),
        (5, 'KR', 'Korean'),
        (6, 'MY', 'Malaysia');


INSERT StateAuditTable
VALUES (1, 'Added'),
        (2, 'Modified'),
        (3, 'Deleted');
