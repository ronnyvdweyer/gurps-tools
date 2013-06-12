using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Reflection;
using System.Text;
using SCv20_Tools.Core.Domain;
using SCv20_Tools.Core.Services;

namespace SCv20_Tools.Core.Data {

    internal class DataContextInitializer : DropCreateDatabaseIfModelChanges<DataContext> {

        protected override void Seed(DataContext context) {
            try {
                LoadCaliber().ForEach(c => context.Calibers.Add(c));
                LoadQualities().ForEach(c => context.Qualities.Add(c));
                LoadObjectiveTypes().ForEach(c => context.ObjectiveTypes.Add(c));

                LoadHistoricalConversion().ForEach(c => context.HistoricalConversions.Add(c));

                context.SaveChanges();

                context.Campaigns.Add(CreateSampleCampaign(context));
                context.Missions.Add(CreateSampleMission(context));
                context.Scenes.Add(CreateSampleScene(context));
            }
            catch (DbEntityValidationException ex) {
                var msg = BuildValidationMessage(ex);
                throw new DbEntityValidationException("Entity Validation Failed - Errors Follow in " + msg);
            }
        }

        private static List<Caliber> LoadCaliber() {
            var list = new List<Caliber> {
                new Caliber { Id = 1, Value = "I",   Reputation =  2,  Networth =  100000M },
                new Caliber { Id = 2, Value = "II",  Reputation =  5,  Networth =  250000M },
                new Caliber { Id = 3, Value = "III", Reputation = 10,  Networth =  500000M },
                new Caliber { Id = 4, Value = "IV",  Reputation = 15,  Networth =  750000M },
                new Caliber { Id = 5, Value = "V",   Reputation = 20,  Networth = 1000000M }
            };

            return list;
        }

        private static List<Quality> LoadQualities() {
            var svc = SerializationService.GetInstance();
            var file = GetSeedResource("SCv20_Tools.Core.Data.Seed.Qualities.json.js");
            var json = svc.Deserialize(file);
            var list = new List<Quality>();

            foreach (var x in json.data) {
                var e = new Quality { BonusAD = x.ad, BonusXP = x.xp, Description = x.description, IsSeasonsOnly = x.season, Name = x.name };
                list.Add(e);
            }

            return list;
        }

        private static List<HistoricalConversion> LoadHistoricalConversion() {
            var list = new List<HistoricalConversion> {
                new HistoricalConversion { Id = 1,  Year = "1875-1900",         Modifier = 0.010M,  Order = 11 },
                new HistoricalConversion { Id = 2,  Year = "1901-1909",         Modifier = 0.025M,  Order = 10 },
                new HistoricalConversion { Id = 3,  Year = "1910-1919",         Modifier = 0.030M,  Order = 09 },
                new HistoricalConversion { Id = 4,  Year = "1920-1929",         Modifier = 0.050M,  Order = 08 },
                new HistoricalConversion { Id = 5,  Year = "1930-1949",         Modifier = 0.060M,  Order = 07 },
                new HistoricalConversion { Id = 6,  Year = "1950-1959",         Modifier = 0.100M,  Order = 06 },
                new HistoricalConversion { Id = 7,  Year = "1960-1969",         Modifier = 0.200M,  Order = 05 },
                new HistoricalConversion { Id = 8,  Year = "1970-1979",         Modifier = 0.300M,  Order = 04 },
                new HistoricalConversion { Id = 9,  Year = "1980-1989",         Modifier = 0.600M,  Order = 03 },
                new HistoricalConversion { Id = 10, Year = "1990-Present Day",  Modifier = 1.000M,  Order = 01 },
                new HistoricalConversion { Id = 11, Year = "2020-Near Future",  Modifier = 1.000M,  Order = 02 }
            };

            return list;
        }

        private static List<ObjectiveType> LoadObjectiveTypes() {
            var list = new List<ObjectiveType>() {
                new ObjectiveType {
                    Id = 01, Name = "Crucial Skill Check",
                    ObjectiveGrades = new List<ObjectiveGrade>() {
                        new ObjectiveGrade {Grade = 1, Description = "Standard skill check (DC 10 + Threat Level); opposed skill check (skill bonus up to 1/2 highest in team)."},
                        new ObjectiveGrade {Grade = 2, Description = "Standard skill check (DC 15 + Threat Level); opposed skill check (skill bonus more than 1/2 highest, up to highest in team)."},
                        new ObjectiveGrade {Grade = 3, Description = "Standard skill check (DC 20 + Threat Level); Complex Task (2–4 Challenges, DC 10 + Threat Level); opposed skill check (skill bonus more than highest in team)."},
                        new ObjectiveGrade {Grade = 4, Description = "Standard skill check (DC 25 + Threat Level); Complex Task (5–7 Challenges, DC 15 + Threat Level); Dramatic Conflict (Lead 2–6, opposing skill bonus up to highest in team)."},
                        new ObjectiveGrade {Grade = 5, Description = "Complex Task (8–10 Challenges, DC 20 + Threat Level); Dramatic Conflict (Lead 7–10, opposing skill bonus more than highest in team)."}
                    }
                },
                new ObjectiveType {
                    Id = 02, Name = "Aid/Advise",
                    ObjectiveGrades = new List<ObjectiveGrade>() {
                        new ObjectiveGrade {Grade = 1, Description = "Ensure a character completes a delicate (but not particularly dangerous) transaction; ensure a government or organization successfully mediates a warmly contested point with a shaky ally."},
                        new ObjectiveGrade {Grade = 2, Description = "Ensure a character completes a delicate and dangerous transaction; ensure a government or organization successfully mediates a hotly contested point with a bitter enemy."},
                        new ObjectiveGrade {Grade = 3, Description = "Ensure a character clears his name (minor crimes or mildly damaging slander); Ensure a government or organization successfully roots out a single well-placed mole."},
                        new ObjectiveGrade {Grade = 4, Description = "Ensure a character clears his name (single felony charge or extremely damaging slander); Ensure a government or organization successfully roots out new or unstable mole network."},
                        new ObjectiveGrade {Grade = 5, Description = "Ensure a character clears his name (multiple felony charges); Ensure a government or organization successfully roots out deeply entrenched and well-informed mole network."}
                    }
                },
                new ObjectiveType {
                    Id = 03, Name = "Acquire",
                    ObjectiveGrades = new List<ObjectiveGrade>() {
                        new ObjectiveGrade {Grade = 1, Description = "Collect/recover a single obscure artifact; hijack a poorly armed civilian transport; steal a poorly defended object from someone whose interest in it is mild."},
                        new ObjectiveGrade {Grade = 2, Description = "Collect/recover several obscure artifacts lost for less than a year; hijack a well-armed private transport; steal a well-defended object from someone whose interest in it is moderate."},
                        new ObjectiveGrade {Grade = 3, Description = "Collect/recover many obscure artifacts lost for up to a decade;hijack a well-armed government transport; steal an intensely defended object from someone whose interest in it is moderate."},
                        new ObjectiveGrade {Grade = 4, Description = "Collect/recover many obscure artifacts lost for up to a century; hijack a well-armed villain transport; steal a well-defended object from someone whose interest in it is fanatic."},
                        new ObjectiveGrade {Grade = 5, Description = "Collect/recover many obscure artifacts lost for millennia; hijack several well-armed transports; steal an intensely defended object from someone whose interest in it is fanatic."}
                    }
                },
                new ObjectiveType {
                    Id = 04, Name = "Capture",
                    ObjectiveGrades = new List<ObjectiveGrade>() {
                        new ObjectiveGrade {Grade = 1, Description = "Arrest an unarmed (but cagy or elusive) minor criminal;abduct a civilian authority supporting the enemy."},
                        new ObjectiveGrade {Grade = 2, Description = "Arrest an armed minor criminal; abduct a trained non-military authority supporting the enemy."},
                        new ObjectiveGrade {Grade = 3, Description = "Arrest an armed felon; abduct a trained military authority supporting the enemy."},
                        new ObjectiveGrade {Grade = 4, Description = "Arrest several armed felons; abduct a trained military combatant supporting the enemy."},
                        new ObjectiveGrade {Grade = 5, Description = "Arrest a criminal mastermind; abduct several trained military combatants supporting the enemy."}
                    }
                },
                new ObjectiveType {
                    Id = 05, Name = "Confirm",
                    ObjectiveGrades = new List<ObjectiveGrade>() {
                        new ObjectiveGrade {Grade = 1, Description = "Confirm the veracity of one defined piece of information, or the motives of one unwitting character."},
                        new ObjectiveGrade {Grade = 2, Description = "Confirm the veracity of several obviously associated (though defined) pieces of information, or the motives of several obviously associated, unwitting characters."},
                        new ObjectiveGrade {Grade = 3, Description = "Confirm the veracity of several seemingly associated and poorly researched pieces of information, or the motives of several seemingly associated, unwitting characters."},
                        new ObjectiveGrade {Grade = 4, Description = "Confirm the veracity of several seemingly unassociated and poorly researched pieces of information, or the motives of several seemingly unassociated unwitting characters."},
                        new ObjectiveGrade {Grade = 5, Description = "Confirm the veracity of several seemingly unassociated and unresearched pieces of information, or the motives of several seemingly unassociated characters aware they’re being evaluated."}
                    }
                },
                new ObjectiveType {
                    Id = 06, Name = "Contain",
                    ObjectiveGrades = new List<ObjectiveGrade>() {
                        new ObjectiveGrade {Grade = 1, Description = "Contain single minor secret amongst small group; cover up minor mission botch in sparsely populated area."},
                        new ObjectiveGrade {Grade = 2, Description = "Contain several minor secrets amongst small group; cover up minor mission botch in well-populated area."},
                        new ObjectiveGrade {Grade = 3, Description = "Contain several minor secrets amongst large group; cover up moderate mission botch in well-populated area."},
                        new ObjectiveGrade {Grade = 4, Description = "Contain several moderate secrets amongst large group; cover up major mission botch in well-populated area."},
                        new ObjectiveGrade {Grade = 5, Description = "Contain one or more major secrets amongst large group; cover up publicized major mission botch."}
                    }
                },
                new ObjectiveType {
                    Id = 07, Name = "Defend",
                    ObjectiveGrades = new List<ObjectiveGrade>() {
                        new ObjectiveGrade {Grade = 1, Description = "Defend single easily protected location or a single attentive NPC against enemies with lower than recommended statistics."},
                        new ObjectiveGrade {Grade = 2, Description = "Defend single hard-to-protect location or a single inattentive NPC against enemies with lower than recommended statistics."},
                        new ObjectiveGrade {Grade = 3, Description = "Defend single hard-to-protect location or a single inattentive NPC against enemies with recommended statistics."},
                        new ObjectiveGrade {Grade = 4, Description = "Defend single easily-protected location or a single attentive NPC against enemies with higher than recommended statistics."},
                        new ObjectiveGrade {Grade = 5, Description = "Defend single hard-to-protect location or a single inattentive NPC against enemies with higher than recommended statistics."}
                    }
                },
                new ObjectiveType {
                    Id = 08, Name = "Destroy",
                    ObjectiveGrades = new List<ObjectiveGrade>() {
                        new ObjectiveGrade {Grade = 1, Description = "Destroy an undefended civilian location or sabotage an undefended civilian vehicle or piece of gear."},
                        new ObjectiveGrade {Grade = 2, Description = "Destroy a poorly defended civilian location or sabotage a poorly defended civilian vehicle or piece of gear."},
                        new ObjectiveGrade {Grade = 3, Description = "Destroy a well-defended civilian location or sabotage a well-defended civilian vehicle or piece of gear."},
                        new ObjectiveGrade {Grade = 4, Description = "Destroy a well-defended military location or sabotage a well-defended military vehicle or piece of gear."},
                        new ObjectiveGrade {Grade = 5, Description = "Destroy a well-defended villain location or sabotage a well-defended villain vehicle or piece of gear."}
                    }
                },
                new ObjectiveType {
                    Id = 09, Name = "Distract",
                    ObjectiveGrades = new List<ObjectiveGrade>() {
                        new ObjectiveGrade {Grade = 1, Description = "Keep 1 person from noticing a quiet incident."},
                        new ObjectiveGrade {Grade = 2, Description = "Keep 1 person from noticing a noisy incident."},
                        new ObjectiveGrade {Grade = 3, Description = "Keep several people from noticing a quiet incident."},
                        new ObjectiveGrade {Grade = 4, Description = "Keep several people from noticing a noisy incident."},
                        new ObjectiveGrade {Grade = 5, Description = "Keep everyone from noticing an obvious incident."}
                    }
                },
                new ObjectiveType {
                    Id = 10, Name = "Evade",
                    ObjectiveGrades = new List<ObjectiveGrade>() {
                        new ObjectiveGrade {Grade = 1, Description = "Escape or avoid capture by inferior opposing force in familiar territory."},
                        new ObjectiveGrade {Grade = 2, Description = "Escape or avoid capture by inferior opposing force in unfamiliar territory."},
                        new ObjectiveGrade {Grade = 3, Description = "Escape or avoid capture by equal opposing force in unfamiliar territory."},
                        new ObjectiveGrade {Grade = 4, Description = "Escape or avoid capture by superior opposing force in unfamiliar territory."},
                        new ObjectiveGrade {Grade = 5, Description = "Escape or avoid capture by superior opposing force in enemy territory."}
                    }
                },
                new ObjectiveType {
                    Id = 11, Name = "Infiltrate",
                    ObjectiveGrades = new List<ObjectiveGrade>() {
                        new ObjectiveGrade {Grade = 1, Description = "Infiltrate an undefended civilian location."},
                        new ObjectiveGrade {Grade = 2, Description = "Infiltrate a poorly defended civilian location."},
                        new ObjectiveGrade {Grade = 3, Description = "Infiltrate a well-defended civilian location."},
                        new ObjectiveGrade {Grade = 4, Description = "Infiltrate a well-defended military location."},
                        new ObjectiveGrade {Grade = 5, Description = "Infiltrate a well-defended villain location."}
                    }
                },
                new ObjectiveType {
                    Id = 12, Name = "Investigate",
                    ObjectiveGrades = new List<ObjectiveGrade>() {
                        new ObjectiveGrade {Grade = 1, Description = "Investigation involves 1 clue, minor research, or minutes-long search or scouting mission."},
                        new ObjectiveGrade {Grade = 2, Description = "Investigation involves 2–4 clues, 1 simple puzzle, moderate research, or an hour-long search or scouting mission."},
                        new ObjectiveGrade {Grade = 3, Description = "Investigation involves 5–6 clues, 1 tricky puzzle, extensive research, or an hours-long search or scouting mission."},
                        new ObjectiveGrade {Grade = 4, Description = "Investigation involves 7–9 clues, 1 complicated puzzle, exhaustive research, or a day-long search or scouting mission."},
                        new ObjectiveGrade {Grade = 5, Description = "Investigation involves 10 clues, 1 nearly impossible puzzle, impeccable research, or a days-long search or scouting mission."}
                    }
                },
                new ObjectiveType {
                    Id = 13, Name = "Neutralize",
                    ObjectiveGrades = new List<ObjectiveGrade>() {
                        new ObjectiveGrade {Grade = 1, Description = "Liquidate or force-retire a civilian authority."},
                        new ObjectiveGrade {Grade = 2, Description = "Liquidate or force-retire a trained non-military authority or several civilian authorities."},
                        new ObjectiveGrade {Grade = 3, Description = "Liquidate or force-retire a trained military authority or several trained non-military authorities."},
                        new ObjectiveGrade {Grade = 4, Description = "Liquidate or force-retire a trained military combatant or several trained military authorities."},
                        new ObjectiveGrade {Grade = 5, Description = "Liquidate or force-retire several trained military combatants."}
                    }
                },
                new ObjectiveType {
                    Id = 14, Name = "Observe",
                    ObjectiveGrades = new List<ObjectiveGrade>() {
                        new ObjectiveGrade {Grade = 1, Description = "Detect 1 activity at a busy or easy-to-navigate location with heavy concealment."},
                        new ObjectiveGrade {Grade = 2, Description = "Detect 1 activity at a quiet or hard-to-navigate location with moderate concealment; detect 2 or more activities at a busy or easy-to-navigate location with heavy concealment."},
                        new ObjectiveGrade {Grade = 3, Description = "Detect 1 activity at a silent or near impossible-to-navigate location with little or no concealment; detect 2 or more activities at a quiet or hard-to-navigate location with moderate concealment."},
                        new ObjectiveGrade {Grade = 4, Description = "Detect 1 activity while evading enemy surveillance or guards; detect 2 or more activities at a silent or near impossible-to-navigate location with little or no concealment."},
                        new ObjectiveGrade {Grade = 5, Description = "Detect 2 or more activities while evading enemy surveillance or guards."}
                    }
                },
                new ObjectiveType {
                    Id = 15, Name = "Recruit",
                    ObjectiveGrades = new List<ObjectiveGrade>() {
                        new ObjectiveGrade {Grade = 1, Description = "Convince an uninvolved, reticent NPC to join a cause with no obvious risk."},
                        new ObjectiveGrade {Grade = 2, Description = "Convince an uninvolved, opposed NPC to join a cause with no obvious risk."},
                        new ObjectiveGrade {Grade = 3, Description = "Convince an uninvolved NPC to join a cause with minor risk; convince an enemy to betray his weak masters."},
                        new ObjectiveGrade {Grade = 4, Description = "Convince an uninvolved NPC to join a cause with moderate risk; convince an enemy to betray his strong masters."},
                        new ObjectiveGrade {Grade = 5, Description = "Convince an uninvolved NPC to join a cause with major risk; convince an enemy to betray his fearsome masters."}
                    }
                },
                new ObjectiveType {
                    Id = 16, Name = "Repair",
                    ObjectiveGrades = new List<ObjectiveGrade>() {
                        new ObjectiveGrade {Grade = 1, Description = "Fix a Simple object with short time frame OR under enemy fire."},
                        new ObjectiveGrade {Grade = 2, Description = "Fix a Simple object with short time frame AND under enemy fire."},
                        new ObjectiveGrade {Grade = 3, Description = "Fix a Complex object with short time frame OR under enemy fire."},
                        new ObjectiveGrade {Grade = 4, Description = "Fix a Complex object with short time frame AND under enemy fire."},
                        new ObjectiveGrade {Grade = 5, Description = "Fix Several Complex objects with short time frame AND under threatening enemy fire."}
                    }
                },
                new ObjectiveType {
                    Id = 17, Name = "Rescue",
                    ObjectiveGrades = new List<ObjectiveGrade>() {
                        new ObjectiveGrade {Grade = 1, Description = "Rescue 1 uninjured character or salvage 1 undamaged vehicle or item from minor environmental danger or enemies with lower than recommended statistics."},
                        new ObjectiveGrade {Grade = 2, Description = "Rescue 1 slightly injured character or salvage 1 slightly damaged vehicle or item from minor environmental danger or enemies with lower than recommended statistics."},
                        new ObjectiveGrade {Grade = 3, Description = "Rescue 1 moderately injured character or salvage 1 moderately damaged vehicle or item from moderate environmental danger or enemies with recommended statistics."},
                        new ObjectiveGrade {Grade = 4, Description = "Rescue 1 moderately injured character or salvage 1 moderately damaged vehicle or item from moderate environmental danger or enemies with higher than recommended statistics."},
                        new ObjectiveGrade {Grade = 5, Description = "Rescue 1 severely injured character or salvage 1 highly damaged vehicle or item from major environmental danger or enemies with higher than recommended statistics."}
                    }
                },
                new ObjectiveType {
                    Id = 18, Name = "Sting",
                    ObjectiveGrades = new List<ObjectiveGrade>() {
                        new ObjectiveGrade {Grade = 1, Description = "Execute thorough, pre-planned operation targeting 1 mark."},
                        new ObjectiveGrade {Grade = 2, Description = "Execute shaky pre-planned operation targeting 1 mark; concoct and execute original plan targeting 1 mark."},
                        new ObjectiveGrade {Grade = 3, Description = "Execute crazy pre-planned operation targeting 1 mark; concoct and execute original plan targeting 2 or more marks."},
                        new ObjectiveGrade {Grade = 4, Description = "Concoct and execute original plan targeting 2 or more marks in short time or with few resources."},
                        new ObjectiveGrade {Grade = 5, Description = "Concoct and execute original plan targeting 2 or more marks in short time and with few resources."}
                    }
                },
                new ObjectiveType {
                    Id = 19, Name = "Test",
                    ObjectiveGrades = new List<ObjectiveGrade>() {
                        new ObjectiveGrade {Grade = 1, Description = "Intentionally test gear with unexpected (but not threatening) side effects."},
                        new ObjectiveGrade {Grade = 2, Description = "Intentionally test gear with side effects that threaten user."},
                        new ObjectiveGrade {Grade = 3, Description = "Intentionally test gear with side effects that threaten user’s entire team; unwittingly test gear with unexpected (but not threatening) side effects."},
                        new ObjectiveGrade {Grade = 4, Description = "Unwittingly test gear with side effects that threaten user."},
                        new ObjectiveGrade {Grade = 5, Description = "Unwittingly test gear with side effects that threaten user’s entire team."}
                    }
                },
                new ObjectiveType {
                    Id = 20, Name = "Transport",
                    ObjectiveGrades = new List<ObjectiveGrade>() {
                        new ObjectiveGrade {Grade = 1, Description = "Publicly transport an easily carried item or piece of information a short distance with little danger."},
                        new ObjectiveGrade {Grade = 2, Description = "Publicly transport an easily carried item or piece of information a moderate distance with mild danger; secretly transport an easily carried item or piece of information a short distance with little danger."},
                        new ObjectiveGrade {Grade = 3, Description = "Publicly transport a difficult-to-carry item or piece of information a moderate distance with mild d anger; secretly transport an easily carried item or piece of information a moderate distance with mild danger."},
                        new ObjectiveGrade {Grade = 4, Description = "Publicly transport an easily carried item or piece of information a great distance with substantial danger; secretly transport a difficult-to-carry item or piece of information a moderate distance with mild danger."},
                        new ObjectiveGrade {Grade = 5, Description = "Publicly transport a difficult-to-carry item or piece of information a great distance with substantial danger; secretly transport any item or piece of information a great distance with substantial danger."}
                    }
                },
            };

            //var svc = SerializationService.GetInstance();
            //svc.SerializeFile(list, "c:\\dalton.json.js");

            return list;
        }

        #region -- Sample Initial Data ------------------------------------------------------------

        private static Campaign CreateSampleCampaign(DataContext ctx) {
            var c = new Campaign {
                BaseNetWorth = 1000000,
                BaseReputation = 2,
                Code = "GITS-00001",
                Concept = "Triller futurista de espionagem evolvendo a existência humana com relação à existência artificial.",
                CreatedOn = DateTime.Now,
                Name = "Ghost in the Shell: Public Security SECTION 9 - The First GIG",
                StartingLevel = 10,
                Summary = "n/a",
                YearId = 11,
                YearDetails = "Futuro alternativo ambientado no ano de 2028.",
                Qualities = new List<CampaignQuality>() {
                    new CampaignQuality { QualityId = 1, CampaignId = 1},
                    //new CampaignQuality { QualityId = 20, CampaignId = 1},
                    //new CampaignQuality { QualityId = 30, CampaignId = 1}
                }
                //Qualities = ctx.Set<Quality>().Where(q => q.Id == 1).ToList()
            };
            return c;
        }

        private Mission CreateSampleMission(DataContext context) {
            var m = new Mission() {
                TotalPartyLevel = 20,
                AdjustedThreatLevel = 1,
                Name = "Break the Ice",
                Code = "GITS-0001",
                CaliberId = 2,
                Motivation = "Suspendisse vitae odio nec felis tincidunt mattis sit. Mauris tristique luctus turpis, ac aliquet magna elementum vel.",
                Briefing = "Mauris tristique luctus turpis, ac aliquet magna elementum vel. Aenean eget velit non dui mattis laoreet. In sodales imperdiet magna laoreet bibendum. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Aenean sollicitudin velit et erat rhoncus molestie. Morbi convallis, felis nec dignissim imperdiet, mi erat feugiat leo, sit amet placerat magna dui sed libero. Donec eget blandit urna. Nullam dui mi, tincidunt nec euismod quis, tincidunt vitae nunc. Etiam auctor scelerisque fringilla. Duis quis aliquet libero.\r\n\r\n" +
                                      "Nunc a purus diam, quis lacinia dolor. Duis urna nisl, malesuada vitae malesuada ac, laoreet in justo. Sed non nunc leo, sed fermentum neque. Aliquam hendrerit ornare leo, at vehicula turpis rutrum eu. Nulla sagittis arcu at dui tristique in commodo risus elementum. Sed aliquam pretium est, vitae porta massa eleifend eget. Fusce a sapien ipsum, ullamcorper commodo nibh. Ut pretium est vel tortor mattis molestie. Vestibulum vulputate dui sed sapien scelerisque sit amet fringilla lacus hendrerit. Nulla ut sapien a mauris vehicula consectetur id vitae nulla.\r\n\r\n" +
                                      "Mauris euismod orci justo, quis eleifend quam. Cras commodo tortor mi, ut imperdiet leo. Etiam semper, ipsum ac pulvinar accumsan, erat sem aliquet lacus, nec elementum nulla quam in sem. Duis fringilla sollicitudin convallis. Integer fermentum mauris orci. Maecenas pulvinar mollis ultrices. Integer viverra, tortor id venenatis tempor, mauris libero consequat dolor, non condimentum massa neque et erat. Fusce sit amet nunc sed erat aliquet euismod nec et orci. Nullam sed erat ligula, ut scelerisque arcu. Proin in nisi magna, id tincidunt erat. Proin et mi at sem euismod ultrices.",
                Qualities = new List<MissionQuality>() {
                    new MissionQuality { QualityId = 3, MissionId = 1 },
                    new MissionQuality { QualityId = 7, MissionId = 1 }
                }
            };

            return m;
        }

        private Scene CreateSampleScene(DataContext ctx) {
            var scene = new Scene {
                MissionID = 1,
                Order = 1,
                Description = "Mauris tristique luctus turpis, ac aliquet magna elementum vel. Aenean eget velit non dui mattis laoreet. In sodales imperdiet magna laoreet bibendum. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Aenean sollicitudin velit et erat rhoncus molestie. Morbi convallis, felis nec dignissim imperdiet, mi erat feugiat leo, sit amet placerat magna dui sed libero. Donec eget blandit urna. Nullam dui mi, tincidunt nec euismod quis, tincidunt vitae nunc. Etiam auctor scelerisque fringilla. Duis quis aliquet libero.\r\n\r\n" +
                              "Nunc a purus diam, quis lacinia dolor. Duis urna nisl, malesuada vitae malesuada ac, laoreet in justo. Sed non nunc leo, sed fermentum neque. Aliquam hendrerit ornare leo, at vehicula turpis rutrum eu. Nulla sagittis arcu at dui tristique in commodo risus elementum. Sed aliquam pretium est, vitae porta massa eleifend eget. Fusce a sapien ipsum, ullamcorper commodo nibh. Ut pretium est vel tortor mattis molestie. Vestibulum vulputate dui sed sapien scelerisque sit amet fringilla lacus hendrerit. Nulla ut sapien a mauris vehicula consectetur id vitae nulla.\r\n\r\n" +
                              "Mauris euismod orci justo, quis eleifend quam. Cras commodo tortor mi, ut imperdiet leo. Etiam semper, ipsum ac pulvinar accumsan, erat sem aliquet lacus, nec elementum nulla quam in sem. Duis fringilla sollicitudin convallis. Integer fermentum mauris orci. Maecenas pulvinar mollis ultrices. Integer viverra, tortor id venenatis tempor, mauris libero consequat dolor, non condimentum massa neque et erat. Fusce sit amet nunc sed erat aliquet euismod nec et orci. Nullam sed erat ligula, ut scelerisque arcu. Proin in nisi magna, id tincidunt erat. Proin et mi at sem euismod ultrices.",
                IsDramatic = true,
                CreatedOn = new DateTime(2013, 06, 01, 20, 00, 00)
            };

            scene.Objectives = new List<SceneObjective>() {
                new SceneObjective {Order = 1, /*CaliberID = 2, ObjectiveTypeID = 1, */ Description  = "Describe the Sample objective #1", GradeId = 2, IsCritical = false, IsPlot = false },
                new SceneObjective {Order = 2, /*CaliberID = 4, ObjectiveTypeID = 4, */ Description  = "Describe the Sample objective #2", GradeId = 14, IsCritical = true,  IsPlot = false },
                new SceneObjective {Order = 3, /*CaliberID = 1, ObjectiveTypeID = 8, */ Description  = "Describe the Sample objective #3", GradeId = 28, IsCritical = false, IsPlot = true }
            };

            return scene;
        }

        private static string GetSeedResource(string resourceID) {
            var assembly = Assembly.GetExecutingAssembly();
            try {
                var stream = new StreamReader(assembly.GetManifestResourceStream(resourceID));
                var contents = stream.ReadToEnd();
                stream.Close();
                stream.Dispose();
                return contents;
            }
            catch (ArgumentNullException ex) {
                throw new InvalidOperationException("Requested Resource [{0}] not found in [{1}].".FormatWith(resourceID, assembly.FullName), ex);
            }
        }

        #endregion -- Sample Initial Data ------------------------------------------------------------

        /// <summary>
        /// Builds a friendly message for the given Repository Validation Erros.
        /// </summary>
        /// <param name="ex">The exception to be parsed into a friendly message.</param>
        /// <returns>String containing the parsed exception messages.</returns>
        public static string BuildValidationMessage(DbEntityValidationException ex) {
            StringBuilder sb = new StringBuilder();
            foreach (var failure in ex.EntityValidationErrors) {
                sb.AppendFormat("'{0}' failed validation.\n", failure.Entry.Entity.GetType());
                foreach (var error in failure.ValidationErrors) {
                    sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }
    }
}