ALTER PROCEDURE [SetKnownGoodState]
AS
BEGIN

delete from AgencyAgent;
--DBCC CHECKIDENT ('[AgencyAgent]', RESEED, 0);
delete from [Location];
DBCC CHECKIDENT ('[Location]', RESEED, 0);
delete from Alias;
DBCC CHECKIDENT ('[Alias]', RESEED, 0);
delete from MissionAgent;
--DBCC CHECKIDENT ('[MissionAgent]', RESEED, 0);
delete from Mission;
DBCC CHECKIDENT ('[Mission]', RESEED, 0);
delete from SecurityClearance;
DBCC CHECKIDENT ('[SecurityClearance]', RESEED, 0);
delete from Agency;
DBCC CHECKIDENT ('[Agency]', RESEED, 0);
delete from Agent;
DBCC CHECKIDENT ('[Agent]', RESEED, 0);

insert into SecurityClearance (SecurityClearanceName) values ('None');
insert into SecurityClearance (SecurityClearanceName) values ('Retired');
insert into SecurityClearance (SecurityClearanceName) values ('Secret');
insert into SecurityClearance (SecurityClearanceName) values ('Top Secret');
insert into SecurityClearance (SecurityClearanceName) values ('Black Ops');

insert into Agency (ShortName, LongName) values ('Gibbins', 'Browsebug');
insert into Agency (ShortName, LongName) values ('Monkhouse', null);
insert into Agency (ShortName, LongName) values ('Nutley', 'Bubblebox');
insert into Agency (ShortName, LongName) values ('Perago', 'Pixoboo');
insert into Agency (ShortName, LongName) values ('Quarton', 'Youspan');
insert into Agency (ShortName, LongName) values ('Mangon', null);
insert into Agency (ShortName, LongName) values ('Tebbit', null);
insert into Agency (ShortName, LongName) values ('Vanderson', 'Jabberstorm');
insert into Agency (ShortName, LongName) values ('Gidman', 'Yamia');
insert into Agency (ShortName, LongName) values ('Dondon', null);
insert into Agency (ShortName, LongName) values ('Mabbett', null);
insert into Agency (ShortName, LongName) values ('Hourihan', 'Ailane');
insert into Agency (ShortName, LongName) values ('Gerlts', 'Mybuzz');
insert into Agency (ShortName, LongName) values ('Cory', null);
insert into Agency (ShortName, LongName) values ('Baskerville', 'DabZ');

insert into Agent (FirstName, LastName, DateOfBirth, Height) values ('Faydra', 'Vamplers', '3/14/1989', 80.24);
insert into Agent (FirstName, LastName, DateOfBirth, Height) values ('Netta', 'Challes', '10/18/1988', 76.82);
insert into Agent (FirstName, LastName, DateOfBirth, Height) values ('Gregorius', 'Mallinar', '10/20/1972', 77.86);
insert into Agent (FirstName, LastName, DateOfBirth, Height) values ('Eli', 'Buie', '10/18/1994', 73.52);
insert into Agent (FirstName, LastName, DateOfBirth, Height) values ('Korry', 'Damper', '3/27/1967', 65.88);
insert into Agent (FirstName, LastName, DateOfBirth, Height) values ('Kaela', 'Ohrtmann', '4/17/1986', 76.78);
insert into Agent (FirstName, LastName, DateOfBirth, Height) values ('Nester', 'Dicty', '10/22/1963', 78.84);
insert into Agent (FirstName, LastName, DateOfBirth, Height) values ('Kenna', 'Demkowicz', '3/2/1978', 82.31);
insert into Agent (FirstName, LastName, DateOfBirth, Height) values ('Nikki', 'Merit', '6/14/1991', 67.52);
insert into Agent (FirstName, LastName, DateOfBirth, Height) values ('Daryl', 'Rowe', '3/14/1973', 62.46);
insert into Agent (FirstName, LastName, DateOfBirth, Height) values ('Carol', 'Fabri', '4/9/1981', 67.77);
insert into Agent (FirstName, LastName, DateOfBirth, Height) values ('Hanny', 'Kienzle', '4/4/1975', 76.58);
insert into Agent (FirstName, LastName, DateOfBirth, Height) values ('Cyril', 'Gatheridge', '10/1/1984', 71.75);
insert into Agent (FirstName, LastName, DateOfBirth, Height) values ('Winslow', 'Bridden', '1/3/1982', 66.18);
insert into Agent (FirstName, LastName, DateOfBirth, Height) values ('Emanuele', 'Mamwell', '1/8/1991', 75.46);

insert into AgencyAgent (AgencyId, AgentId, SecurityClearanceId, BadgeId, ActivationDate, DeactivationDate, IsActive) values (1, 1, 1, '0771ef06-cf1d-4429-a117-9795d27e6723', '1/4/2000', '9/23/2021', 1);
insert into AgencyAgent (AgencyId, AgentId, SecurityClearanceId, BadgeId, ActivationDate, DeactivationDate, IsActive) values (2, 2, 2, '05814c1b-2d96-4584-897f-7eba9c2caf3d', '4/9/2007', '2/28/2013', 0);
insert into AgencyAgent (AgencyId, AgentId, SecurityClearanceId, BadgeId, ActivationDate, DeactivationDate, IsActive) values (3, 3, 3, 'a68b4bf9-80af-4886-99f0-13280886803e', '7/30/2004', '5/1/2003', 0);
insert into AgencyAgent (AgencyId, AgentId, SecurityClearanceId, BadgeId, ActivationDate, DeactivationDate, IsActive) values (4, 4, 4, 'b19b1fdf-a040-4100-aa47-9c79f945a740', '4/27/2014', '7/23/2011', 0);
insert into AgencyAgent (AgencyId, AgentId, SecurityClearanceId, BadgeId, ActivationDate, DeactivationDate, IsActive) values (5, 5, 5, '4f41af99-06cb-429c-97eb-69c572c823ed', '12/5/2020', '8/19/2001', 0);
insert into AgencyAgent (AgencyId, AgentId, SecurityClearanceId, BadgeId, ActivationDate, DeactivationDate, IsActive) values (6, 6, 1, '6cc8720d-31c5-43ca-b946-4e68c3e83551', '12/19/2019', '9/27/2008', 0);
insert into AgencyAgent (AgencyId, AgentId, SecurityClearanceId, BadgeId, ActivationDate, DeactivationDate, IsActive) values (7, 7, 2, '356f6631-ecf1-449d-b2e2-ec6d5ad65adf', '10/19/2019', '10/17/2005', 1);
insert into AgencyAgent (AgencyId, AgentId, SecurityClearanceId, BadgeId, ActivationDate, DeactivationDate, IsActive) values (8, 8, 3, 'd36c1659-7b3d-44cc-bed3-7171618e59e1', '10/10/2004', '7/28/2017', 1);
insert into AgencyAgent (AgencyId, AgentId, SecurityClearanceId, BadgeId, ActivationDate, DeactivationDate, IsActive) values (9, 9, 4, '63a4ebce-c10d-4372-ac14-d478587dcd34', '7/20/2016', '12/16/2019', 1);
insert into AgencyAgent (AgencyId, AgentId, SecurityClearanceId, BadgeId, ActivationDate, DeactivationDate, IsActive) values (10, 10, 5, 'a80ca801-583c-42df-a234-0754fd8dad1c', '12/14/2009', '8/30/2000', 1);
insert into AgencyAgent (AgencyId, AgentId, SecurityClearanceId, BadgeId, ActivationDate, DeactivationDate, IsActive) values (11, 11, 1, 'b621ccd1-de55-4332-b0c8-da8e2498860f', '11/2/2019', '6/18/2012', 1);
insert into AgencyAgent (AgencyId, AgentId, SecurityClearanceId, BadgeId, ActivationDate, DeactivationDate, IsActive) values (12, 12, 2, 'cd20f402-0ccc-4a81-a0ed-c21b2b47da57', '12/19/2013', '8/19/2000', 1);
insert into AgencyAgent (AgencyId, AgentId, SecurityClearanceId, BadgeId, ActivationDate, DeactivationDate, IsActive) values (13, 13, 3, '76a880da-4d95-41b4-af4a-1e1fff8aa1f3', '8/16/2005', '7/24/2004', 1);
insert into AgencyAgent (AgencyId, AgentId, SecurityClearanceId, BadgeId, ActivationDate, DeactivationDate, IsActive) values (14, 14, 4, 'f3b65150-4e98-4cd4-8698-7132f567acfb', '10/5/2010', '2/23/2013', 0);
insert into AgencyAgent (AgencyId, AgentId, SecurityClearanceId, BadgeId, ActivationDate, DeactivationDate, IsActive) values (15, 15, 5, '1d2f8a2e-998b-4d82-bf8b-3b82095ab950', '9/14/2008', '10/24/2017', 0);

insert into Alias (AgentId, AliasName, InterpolId, Persona) values (1, 'ylogsdale0', '745a1b47-a3e5-464d-93ec-3fff806c07b2', 'Serviceberry');
insert into Alias (AgentId, AliasName, InterpolId, Persona) values (2, 'tmocquer1', null, 'Black Didymodon Moss');
insert into Alias (AgentId, AliasName, InterpolId, Persona) values (3, 'swardle2', '9b029a8c-903b-4c16-ab7d-cbe515be3e95', 'Wyoming Townsend Daisy');
insert into Alias (AgentId, AliasName, InterpolId, Persona) values (4, 'ndavids3', null, null);
insert into Alias (AgentId, AliasName, InterpolId, Persona) values (5, 'ddiffley4', null, null);
insert into Alias (AgentId, AliasName, InterpolId, Persona) values (6, 'cantonnikov5', null, null);
insert into Alias (AgentId, AliasName, InterpolId, Persona) values (7, 'jwalcot6', '714cc39a-7abf-4b6b-9dbf-f3228cffa8f0', 'Cliff Bedstraw');
insert into Alias (AgentId, AliasName, InterpolId, Persona) values (8, 'mjeremaes7', '998cf287-d4ef-4169-bd98-9362b69476f0', 'Wingleaf Soapberry');
insert into Alias (AgentId, AliasName, InterpolId, Persona) values (9, 'tfedder8', null, null);
insert into Alias (AgentId, AliasName, InterpolId, Persona) values (10, 'brippon9', null, null);
insert into Alias (AgentId, AliasName, InterpolId, Persona) values (11, 'astattona', '7802bd97-dca9-4678-ab13-b4c81c400dd8', 'Smooth Yellow Vetch');
insert into Alias (AgentId, AliasName, InterpolId, Persona) values (12, 'gwymerb', 'a038d6b8-df86-4b0f-87ba-8c6a77c21c67', null);
insert into Alias (AgentId, AliasName, InterpolId, Persona) values (13, 'lcomelloc', 'f3b5f698-35a0-49d8-8e3e-3c1a22b20b01', 'Common Woolly Sunflower');
insert into Alias (AgentId, AliasName, InterpolId, Persona) values (14, 'mpogosiand', 'f436cc84-0d08-4971-9a31-7c38d8837bc5', 'Black Garlic');
insert into Alias (AgentId, AliasName, InterpolId, Persona) values (15, 'oscotsone', 'fbcc3e6b-ba3f-4e48-9245-8bb64f473281', 'California Rockcress');

insert into [Location] (AgencyId, LocationName, Street1, Street2, City, PostalCode, CountryCode) values (1, 'Lydall, Inc.', '19453 Grasskamp Court', null, 'Guaíba', '6897', 1);
insert into [Location] (AgencyId, LocationName, Street1, Street2, City, PostalCode, CountryCode) values (2, 'Easterly Government Properties, Inc.', '8658 Fieldstone Alley', '8592', 'Biecz', '2', 2);
insert into [Location] (AgencyId, LocationName, Street1, Street2, City, PostalCode, CountryCode) values (3, 'McDermott International, Inc.', '5 Bluejay Terrace', null, 'Pirca', '09', 3);
insert into [Location] (AgencyId, LocationName, Street1, Street2, City, PostalCode, CountryCode) values (4, 'Brookfield Renewable Partners L.P.', '8 Dahle Hill', null, 'Paris La Défense', '6', 4);
insert into [Location] (AgencyId, LocationName, Street1, Street2, City, PostalCode, CountryCode) values (5, 'State Street Corporation', '86859 Nevada Street', null, 'Khānaqāh', '0218', 5);
insert into [Location] (AgencyId, LocationName, Street1, Street2, City, PostalCode, CountryCode) values (6, 'First Horizon National Corporation', '92130 Weeping Birch Drive', '9122', 'Ćićevac', '105', 6);
insert into [Location] (AgencyId, LocationName, Street1, Street2, City, PostalCode, CountryCode) values (7, 'ConAgra Brands, Inc.', '60343 Riverside Junction', '41788', 'Suruhan', '86689', 7);
insert into [Location] (AgencyId, LocationName, Street1, Street2, City, PostalCode, CountryCode) values (8, 'FSB Bancorp, Inc.', '19 Northwestern Hill', null, 'Wang Nam Khiao', '895', 8);
insert into [Location] (AgencyId, LocationName, Street1, Street2, City, PostalCode, CountryCode) values (9, 'ABAXIS, Inc.', '470 2nd Way', null, 'Ar Ruḩaybah', '83138', 9);
insert into [Location] (AgencyId, LocationName, Street1, Street2, City, PostalCode, CountryCode) values (10, 'Sophiris Bio, Inc.', '872 Golden Leaf Terrace', '3915', 'Taoshan', '92', 10);
insert into [Location] (AgencyId, LocationName, Street1, Street2, City, PostalCode, CountryCode) values (11, 'PT Telekomunikasi Indonesia, Tbk', '2 Spenser Way', null, 'Radnice', '44410', 11);
insert into [Location] (AgencyId, LocationName, Street1, Street2, City, PostalCode, CountryCode) values (12, 'BT Group plc', '078 Blue Bill Park Point', '419', 'Tongyang', '3733', 12);
insert into [Location] (AgencyId, LocationName, Street1, Street2, City, PostalCode, CountryCode) values (13, 'Silver Run Acquisition Corporation II', '51 Birchwood Point', '0', 'Kakhovka', '4565', 13);
insert into [Location] (AgencyId, LocationName, Street1, Street2, City, PostalCode, CountryCode) values (14, 'SPDR Dorsey Wright Fixed Income Allocation ETF', '786 Mariners Cove Court', '922', 'Tangba', '48442', 14);
insert into [Location] (AgencyId, LocationName, Street1, Street2, City, PostalCode, CountryCode) values (15, 'Amphenol Corporation', '29 Pankratz Court', '5383', 'Oklahoma City', '03375', 15);

insert into Mission (AgencyId, CodeName, StartDate, ProjectedEndDate, ActualEndDate, OperationalCost, Notes) values (1, 'Jordanna', '12/13/2019', '3/4/2015', '9/22/2016', 2612.88, null);
insert into Mission (AgencyId, CodeName, StartDate, ProjectedEndDate, ActualEndDate, OperationalCost, Notes) values (2, 'Derby', '5/21/2004', '5/29/2003', '11/20/2018', 964.16, null);
insert into Mission (AgencyId, CodeName, StartDate, ProjectedEndDate, ActualEndDate, OperationalCost, Notes) values (3, 'Tonye', '4/18/2011', '7/31/2015', '11/5/2020', 1816.08, null);
insert into Mission (AgencyId, CodeName, StartDate, ProjectedEndDate, ActualEndDate, OperationalCost, Notes) values (4, 'Jaimie', '3/26/2014', '5/23/2011', '8/9/2005', 2466.23, 'Cebus apella');
insert into Mission (AgencyId, CodeName, StartDate, ProjectedEndDate, ActualEndDate, OperationalCost, Notes) values (5, 'Lacy', '1/26/2022', '5/17/2011', '9/22/2009', 113.92, 'Ammospermophilus nelsoni');
insert into Mission (AgencyId, CodeName, StartDate, ProjectedEndDate, ActualEndDate, OperationalCost, Notes) values (6, 'Laurena', '1/24/2014', '11/22/2009', '12/16/2009', 637.91, 'Dusicyon thous');
insert into Mission (AgencyId, CodeName, StartDate, ProjectedEndDate, ActualEndDate, OperationalCost, Notes) values (7, 'Bobby', '9/1/2010', '10/31/2015', '9/22/2008', 700.53, 'Deroptyus accipitrinus');
insert into Mission (AgencyId, CodeName, StartDate, ProjectedEndDate, ActualEndDate, OperationalCost, Notes) values (8, 'Hannis', '1/15/2008', '9/18/2001', '7/27/2001', 1814.38, 'Geochelone elephantopus');
insert into Mission (AgencyId, CodeName, StartDate, ProjectedEndDate, ActualEndDate, OperationalCost, Notes) values (9, 'Marilyn', '6/8/2016', '3/5/2007', null, null, null);
insert into Mission (AgencyId, CodeName, StartDate, ProjectedEndDate, ActualEndDate, OperationalCost, Notes) values (10, 'Peadar', '1/16/2012', '4/28/2005', '9/24/2017', 2448.52, null);
insert into Mission (AgencyId, CodeName, StartDate, ProjectedEndDate, ActualEndDate, OperationalCost, Notes) values (11, 'Elvera', '1/31/2012', '1/11/2015', '7/27/2002', 654.7, 'Psophia viridis');
insert into Mission (AgencyId, CodeName, StartDate, ProjectedEndDate, ActualEndDate, OperationalCost, Notes) values (12, 'Ameline', '8/7/2015', '12/17/2015', '7/8/2005', 531.21, 'Globicephala melas');
insert into Mission (AgencyId, CodeName, StartDate, ProjectedEndDate, ActualEndDate, OperationalCost, Notes) values (13, 'Frasquito', '11/12/2019', '9/11/2005', '10/8/2021', 2390.22, 'Bassariscus astutus');
insert into Mission (AgencyId, CodeName, StartDate, ProjectedEndDate, ActualEndDate, OperationalCost, Notes) values (14, 'Zulema', '3/16/2015', '9/12/2002', '3/27/2018', 1273.52, null);
insert into Mission (AgencyId, CodeName, StartDate, ProjectedEndDate, ActualEndDate, OperationalCost, Notes) values (15, 'Hewitt', '12/18/2010', '5/27/2007', '1/28/2013', 138.75, 'Vombatus ursinus');

insert into MissionAgent (MissionId, AgentId) values (1, 1);
insert into MissionAgent (MissionId, AgentId) values (15, 1);
insert into MissionAgent (MissionId, AgentId) values (2, 2);
insert into MissionAgent (MissionId, AgentId) values (3, 3);
insert into MissionAgent (MissionId, AgentId) values (4, 4);
insert into MissionAgent (MissionId, AgentId) values (5, 5);
insert into MissionAgent (MissionId, AgentId) values (6, 6);
insert into MissionAgent (MissionId, AgentId) values (7, 7);
insert into MissionAgent (MissionId, AgentId) values (8, 8);
insert into MissionAgent (MissionId, AgentId) values (9, 9);
insert into MissionAgent (MissionId, AgentId) values (10, 10);
insert into MissionAgent (MissionId, AgentId) values (11, 11);
insert into MissionAgent (MissionId, AgentId) values (12, 12);
insert into MissionAgent (MissionId, AgentId) values (13, 13);
insert into MissionAgent (MissionId, AgentId) values (14, 14);
insert into MissionAgent (MissionId, AgentId) values (15, 15);



END;