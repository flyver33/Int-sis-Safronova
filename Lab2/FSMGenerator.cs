namespace Lab2;

class FSMGenerator {

    private int maxFloor;
    private Dictionary<string, Dictionary<string, string>> fSM = new();

    public FSMGenerator(int maxFloor) {
        this.maxFloor = maxFloor;

    }

    private void AddMaxMinFloorState(int floor, int nextFloor, string direction) {
        fSM.Add(floor.ToString() + "ss", []);
        fSM.Add(floor.ToString() + "ms", []);

        List<string> states = new(){"ss", "ms"};

        states.ForEach(state => {

            fSM[floor.ToString() + state].Add("__", floor.ToString() + "ss");
            fSM[floor.ToString() + state].Add("_" + direction, nextFloor.ToString() + direction);

            List<string> dirs = new(){"_", direction};

            dirs.ForEach(dir => {

                List<int> floors1tothis = Enumerable.Range(1, floor - 1).ToList();

                floors1tothis.ForEach(fl => {
                    fSM[floor.ToString() + state].Add(fl.ToString() + dir, nextFloor.ToString() + direction);
                });

                List<int> floorsthistomax = Enumerable.Range(floor + 1, maxFloor - floor).ToList();

                floorsthistomax.ForEach(fl => {
                    fSM[floor.ToString() + state].Add(fl.ToString() + dir, nextFloor.ToString() + direction);
                });
            });

        }); // для уезда и стоянки

    }

    private void AddUpDownState() {

        List<int> floorswominmax = Enumerable.Range(3, maxFloor - 3 - 1).ToList();
        
        floorswominmax.ForEach(floor => {

            List<string> dirs = new(){"_", "up", "dn"};

            dirs.ForEach(dir => {
                fSM[floor.ToString() + "up"].Add(floor.ToString() + dir, floor.ToString() + "up" + "s");
                fSM[floor.ToString() + "dn"].Add(floor.ToString() + dir, floor.ToString() + "dn" + "s");
            }); // curFloor anyDir

            List<int> floorsthistomax = Enumerable.Range(floor + 1, maxFloor - floor).ToList();

            floorsthistomax.ForEach(fl => {

                List<string> dirs = new(){"_", "dn"};

                dirs.ForEach(dir => {
                    fSM[floor.ToString() + "up"].Add(fl.ToString() + dir, (floor + 1).ToString() + "up");
                });

                fSM[floor.ToString() + "up"].Add(fl.ToString() + "up", floor.ToString() + "up" + "s");
            }); // upperFloor _ || upperFloor down скипаем и едем вверх и останавливаемся если upperFloor up

            List<int> floors1tothis = Enumerable.Range(1, floor - 1).ToList();

            floors1tothis.ForEach(fl => {

                List<string> dirs = new(){"_", "up"};

                dirs.ForEach(dir => {
                    fSM[floor.ToString() + "dn"].Add(fl.ToString() + dir, (floor - 1).ToString() + "dn");
                });

                fSM[floor.ToString() + "dn"].Add(fl.ToString() + "dn", floor.ToString() + "dn" + "s");
            }); // lowerFloor _ || lowerFloor up едем вниз и останавливаемся если lowerFloor dn
        });

        List<int> minmaxfloors = new() { 2, maxFloor - 1};

        minmaxfloors.ForEach(floor => {

            List<string> dirs = new(){"_", "up", "dn"};

                dirs.ForEach(dir => {
                fSM[floor.ToString() + "up"].Add(floor.ToString() + dir, floor.ToString() + "up" + "s");
                fSM[floor.ToString() + "dn"].Add(floor.ToString() + dir, floor.ToString() + "dn" + "s");
            });
        }); // minMaxFloors currentFloor

        List<int> upperfloors = Enumerable.Range(3, maxFloor - 3 + 1).ToList();

        upperfloors.ForEach(fl => {

                List<string> dirs = new(){"_", "dn"};

                dirs.ForEach(dir => {
                    fSM[2.ToString() + "up"].Add(fl.ToString() + dir, 3.ToString() + "up");
                });

                fSM[2.ToString() + "up"].Add(fl.ToString() + "up", 2.ToString() + "up" + "s");
        });

        List<int> lowerfloors = Enumerable.Range(1, maxFloor - 1 - 1).ToList();

        lowerfloors.ForEach(fl => {
                
                List<string> dirs = new(){"_", "up"};

                dirs.ForEach(dir => {
                    fSM[(maxFloor - 1).ToString() + "dn"].Add(fl.ToString() + dir, (maxFloor - 2).ToString() + "dn");
                });

                fSM[(maxFloor - 1).ToString() + "dn"].Add(fl.ToString() + "dn", (maxFloor - 1).ToString() + "dn" + "s");
        });

        fSM[2.ToString() + "dn"].Add(1.ToString() + "up", 1.ToString() + "ms");
        fSM[(maxFloor - 1).ToString() + "up"].Add(maxFloor.ToString() + "dn", maxFloor.ToString() + "ms"); // minMaxFloors oppositeDirs

        fSM[2.ToString() + "dn"].Add(1.ToString() + "_", 1.ToString() + "ms");
        fSM[(maxFloor - 1).ToString() + "up"].Add(maxFloor.ToString() + "_", maxFloor.ToString() + "ms"); // minMaxFloors _

    }

    private void AddUpDownStillState() {

        List<int> floorswominmax = Enumerable.Range(3, maxFloor - 3 - 1).ToList();

        List<string> dirs = new(){"_", "dn", "up"};
        
        floorswominmax.ForEach(floor => {
            
            fSM[floor.ToString() + "up" + "s"].Add("__", floor.ToString() + "ss");
            fSM[floor.ToString() + "dn" + "s"].Add("__", floor.ToString() + "ss");

            List<string> dirsdnup = new(){"dn", "up"};

            dirsdnup.ForEach(dir => {
                fSM[floor.ToString() + "up" + "s"].Add("_" + dir, floor.ToString() + dir);
                fSM[floor.ToString() + "dn" + "s"].Add("_" + dir, floor.ToString() + dir);
            });

            dirs.ForEach(dir => {

                List<int> upperfloors = Enumerable.Range(floor + 1, maxFloor - floor).ToList();
                upperfloors.ForEach(fl => {
                    fSM[floor.ToString() + "up" + "s"].Add(fl.ToString() + dir, (floor + 1).ToString() + "up");
                });

                List<int> lowerfloors = Enumerable.Range(1, floor - 1).ToList();
                lowerfloors.ForEach(fl => {
                    fSM[floor.ToString() + "dn" + "s"].Add(fl.ToString() + dir, (floor - 1).ToString() + "dn");
                });

                fSM[floor.ToString() + "dn" + "s"].Add(floor.ToString() + dir, floor.ToString() + "dn" + "s");
                fSM[floor.ToString() + "up" + "s"].Add(floor.ToString() + dir, floor.ToString() + "up" + "s");
            });
        });

        dirs.ForEach(dir => {
            List<int> upperfloors = Enumerable.Range(3, maxFloor - 3 + 1).ToList();
            upperfloors.ForEach(fl => {
                fSM[2.ToString() + "up" + "s"].Add(fl.ToString() + dir, 3.ToString() + "up");
            });
            
            List<int> lowerfloors = Enumerable.Range(1, maxFloor - 2).ToList();
            lowerfloors.ForEach(fl => {
                fSM[(maxFloor - 1).ToString() + "dn" + "s"].Add(fl.ToString() + dir, (maxFloor  - 2).ToString() + "dn");
            });

            fSM[2.ToString() + "dn" + "s"].Add(2.ToString() + dir, 2.ToString() + "dn" + "s");
            fSM[2.ToString() + "up" + "s"].Add(2.ToString() + dir, 2.ToString() + "up" + "s");
            fSM[(maxFloor - 1).ToString() + "up" + "s"].Add((maxFloor - 1).ToString() + dir, (maxFloor - 1).ToString() + "up" + "s");
            fSM[(maxFloor - 1).ToString() + "dn" + "s"].Add((maxFloor - 1).ToString() + dir, (maxFloor - 1).ToString() + "dn" + "s");
        });

        fSM[2.ToString() + "up" + "s"].Add("__", 2.ToString() + "ss");
        fSM[(maxFloor - 1).ToString() + "up" + "s"].Add("__", (maxFloor - 1).ToString() + "ss");

        fSM[2.ToString() + "dn" + "s"].Add("__", 2.ToString() + "ss");
        fSM[(maxFloor - 1).ToString() + "dn" + "s"].Add("__", (maxFloor - 1).ToString() + "ss");

        List<string> dirsdnup = new(){"dn", "up"};

        dirsdnup.ForEach(dir => {
                fSM[2.ToString() + "up" + "s"].Add("_" + dir, 2.ToString() + dir);
                fSM[(maxFloor - 1).ToString() + "up" + "s"].Add("_" + dir, (maxFloor - 1).ToString() + dir);
                
                fSM[2.ToString() + "dn" + "s"].Add("_" + dir, 2.ToString() + dir);
                fSM[(maxFloor - 1).ToString() + "dn" + "s"].Add("_" + dir, (maxFloor - 1).ToString() + dir);
            });

        fSM[2.ToString() + "dn" + "s"].Add(1.ToString() + "_", 1.ToString() + "ms");
        fSM[2.ToString() + "dn" + "s"].Add(1.ToString() + "up", 1.ToString() + "ms");
        fSM[(maxFloor - 1).ToString() + "up" + "s"].Add(maxFloor.ToString() + "_", maxFloor.ToString() + "ms");
        fSM[(maxFloor - 1).ToString() + "up" + "s"].Add(maxFloor.ToString() + "dn", maxFloor.ToString() + "ms");

    }

    private void AddStillStillState() {

        List<int> floors = Enumerable.Range(2, maxFloor - 2).ToList();

        floors.ForEach(floor => {
            fSM[floor.ToString() + "ss"].Add("__", floor.ToString() + "ss");

            fSM[floor.ToString() + "ss"].Add("_" + "up", (floor + 1).ToString() + "up");
            fSM[floor.ToString() + "ss"].Add("_" + "dn", (floor - 1).ToString() + "dn");

            List<string> dirs = new(){"_", "dn", "up"};

            dirs.ForEach(dir => {

                List<int> upperfloors = Enumerable.Range(floor + 1, maxFloor - floor).ToList();

                upperfloors.ForEach(fl => {
                    fSM[floor.ToString() + "ss"].Add(fl.ToString() + dir, (floor + 1).ToString() + "up");
                });

                List<int> lowerfloors = Enumerable.Range(1, floor - 1).ToList();

                lowerfloors.ForEach(fl => {
                    fSM[floor.ToString() + "ss"].Add(fl.ToString() + dir, (floor - 1).ToString() + "dn");
                });
            });
        });
    }
    

    public Dictionary<string, Dictionary<string, string>> GenerateFSM() {
        AddMaxMinFloorState(maxFloor, maxFloor-1, "dn");
        AddMaxMinFloorState(1, 2, "up");

        List<int> floors = Enumerable.Range(2, maxFloor - 2).ToList();

        floors.ForEach(floor => {

            List<string> dirs = new(){"ss", "up", "dn", "ups", "dns"};

            dirs.ForEach(dir => {
                fSM.Add(floor.ToString() + dir, []);
            });
        });

        AddUpDownState();

        AddUpDownStillState();

        AddStillStillState();

        return fSM;
    }

    public Dictionary<int, string> GenerateFloorsButtons() {
        Dictionary<int, string> floorsButtons = [];

        List<int> floors = Enumerable.Range(1, maxFloor).ToList();

        floors.ForEach(i => {
            floorsButtons[i] = "_";
        });

        return floorsButtons;
    }

    public Dictionary<int, Dictionary<int, string>> GenerateDirections() {
        Dictionary<int, Dictionary<int, string>> directions = new();

        List<int> floors = Enumerable.Range(1, maxFloor).ToList();

        floors.ForEach(floor => {
            directions.Add(floor, []);

            List<int> lowerfloors = Enumerable.Range(1, floor).ToList();

            lowerfloors.ForEach(fl => {
                directions[floor].Add(fl, "dn");
            });

            List<int> upperfloors = Enumerable.Range(floor + 1, maxFloor - floor).ToList();

            upperfloors.ForEach(fl =>  {
                directions[floor].Add(fl, "up");
            });
        });

        return directions;
    }
    
}