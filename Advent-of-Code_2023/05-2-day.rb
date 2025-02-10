# Open file and make an array wiht line break as separation.
#file_data = File.read("04-1-in.txt")
file = File.open("05-in.txt").read

sample = "seeds: 79 14 55 13

seed-to-soil map:
50 98 2
52 50 48

soil-to-fertilizer map:
0 15 37
37 52 2
39 0 15

fertilizer-to-water map:
49 53 8
0 11 42
42 0 7
57 7 4

water-to-light map:
88 18 7
18 25 70

light-to-temperature map:
45 77 23
81 45 19
68 64 13

temperature-to-humidity map:
0 69 1
1 0 69

humidity-to-location map:
60 56 37
56 93 4"

source = file
array = []

# seeds
array = source.split("seed-to-soil map:\n")
seeds = array[0].split("seeds:").last.split(" ").map { |s| s.to_i }
seeds

# seedToSoilMap
seedToSoilMap = []
array = array[1].split("soil-to-fertilizer map:\n")
array[0].each_line do |line|
  seedToSoilMap << line.split(" ").map { |s| s.to_i } if line.split(" ") != []
end
seedToSoilMap

# soil-to-fertilizer map:
soilToFertilizerMap = []
array = array[1].split("fertilizer-to-water map:\n")
array[0].each_line do |line|
  soilToFertilizerMap << line.split(" ").map { |s| s.to_i } if line.split(" ") != []
end
soilToFertilizerMap

# fertilizer-to-water map
fertilizerToWaterMap = []
array = array[1].split("water-to-light map:\n")
array[0].each_line do |line|
  fertilizerToWaterMap << line.split(" ").map { |s| s.to_i } if line.split(" ") != []
end
fertilizerToWaterMap

# water-to-light map:
waterToLightMap = []
array = array[1].split("light-to-temperature map:\n")
array[0].each_line do |line|
  waterToLightMap << line.split(" ").map { |s| s.to_i } if line.split(" ") != []
end
waterToLightMap

#light-to-temperature map:
lightToTemperatureMap = []
array = array[1].split("temperature-to-humidity map:\n")
array[0].each_line do |line|
  lightToTemperatureMap << line.split(" ").map { |s| s.to_i } if line.split(" ") != []
end
lightToTemperatureMap

#temperature-to-humidity map:
temperatureToHumidityMap = []
array = array[1].split("humidity-to-location map:\n")
array[0].each_line do |line|
  temperatureToHumidityMap << line.split(" ").map { |s| s.to_i } if line.split(" ") != []
end
temperatureToHumidityMap

#humidity-to-location map:
humidityToLocationMap = []
array[1].each_line do |line|
  humidityToLocationMap << line.split(" ").map { |s| s.to_i } if line.split(" ") != []
end
humidityToLocationMap

def find_mapping(source, array)
  array.each do |arr|
    range = (arr[1]..(arr[1] + arr[2]))
    included = range.include?(source)
    if included
      arr
      gap = source - arr[1]
      dest = arr[0] + gap
      return dest
    end
  end
  return source
end

almanach = []
#seed-to-soil

# s = seeds.each_slice(2).to_a
s = [[3943078016, 158366385], [481035699, 103909769], [3553279107, 15651230], [3322093486, 189601966], [2957349913, 359478652], [924423181, 691197498], [2578953067, 27362630], [124747783, 108079254], [1992340665, 437203822], [2681092979, 110901631]]

s = [[2957349913, 35947865], [2993297778, 35947865], [3029245643, 35947865], [3065193508, 35947865], [3101141373, 35947865], [3137089238, 35947865], [3173037103, 35947865], [3208984968, 35947865], [3244932833, 35947865], [3280880698, 35947865]]
s = [[3173037103, 35947865]]
ar = []
s.each do |s|
  #  2000.times do
  p s[1]
  x = s[1] / 1
  x.times do
    ar << s[0]
    s[0] = s[0] + 1
  end
end

seeds_ = ar

seeds_.each do |seed|
  soil = find_mapping(seed, seedToSoilMap)
  soil
  fertilizer = find_mapping(soil, soilToFertilizerMap)
  fertilizer
  water = find_mapping(fertilizer, fertilizerToWaterMap)
  water
  ligth = find_mapping(water, waterToLightMap)
  ligth
  temperature = find_mapping(ligth, lightToTemperatureMap)
  temperature
  humidity = find_mapping(temperature, temperatureToHumidityMap)
  humidity
  location = find_mapping(humidity, humidityToLocationMap)
  location
  #almanach << [seed, soil, fertilizer, water, ligth, temperature, humidity, location]

  almanach << location
end

#almanach.sort_by { |arr| arr[1] }
p "result"
p almanach.sort.first
