file_data = File.read("16-in.txt").split
#p file_data.count

test = File.read("16-run.txt").split

run = file_data
p [run.first.length, run.length]
require "set"

$beams = Set.new
$energized = Set.new

$caves = run.map { |arr| arr.chars }

def limits(x, y)
  x >= 0 && y >= 0 && !$caves[y].nil? && !$caves[y][x].nil?
end

def left(x, y)
  b = $caves[y][x]
  case b
  when ".", "-"
    $routes << [:left, x - 1, y]
  when "|"
    $routes << [:up, x, y - 1] unless $beams.include?([:left, x, y])
    $routes << [:down, x, y + 1] unless $beams.include?([:left, x, y])
  when "/"
    $routes << [:down, x, y + 1]
  when "\\"
    $routes << [:up, x, y - 1]
  end
  $beams.add([:left, x, y])
  $energized.add([x, y])
end

def up(x, y)
  b = $caves[y][x]
  case b
  when ".", "|"
    $routes << [:up, x, y - 1]
  when "-"
    $routes << [:left, x - 1, y] unless $beams.include?([:up, x, y])
    $routes << [:right, x + 1, y] unless $beams.include?([:up, x, y])
  when "/"
    $routes << [:right, x + 1, y]
  when "\\"
    $routes << [:left, x - 1, y]
  end
  $beams.add([:up, x, y])
  $energized.add([x, y])
end

def down(x, y)
  b = $caves[y][x]
  case b
  when ".", "|"
    $routes << [:down, x, y + 1]
  when "-"
    $routes << [:right, x + 1, y] unless $beams.include?([:down, x, y])
    $routes << [:left, x - 1, y] unless $beams.include?([:down, x, y])
  when "/"
    $routes << [:left, x - 1, y]
  when "\\"
    $routes << [:right, x + 1, y]
  end
  $beams.add([:down, x, y])
  $energized.add([x, y])
end

def right(x, y)
  b = $caves[y][x]
  case b
  when ".", "-"
    $routes << [:right, x + 1, y]
  when "|"
    $routes << [:up, x, y - 1] unless $beams.include?([:right, x, y])
    $routes << [:down, x, y + 1] unless $beams.include?([:right, x, y])
  when "/"
    $routes << [:up, x, y - 1]
  when "\\"
    $routes << [:down, x, y + 1]
  end
  $beams.add([:right, x, y])
  $energized.add([x, y])
end

def draw
  $caves.each do |rr|
    p rr
  end
  $energized.each do |set|
    $caves[set.last][set.first] = "#"
  end

  p "--------------------------------------------"
  $caves.each do |r|
    p r
  end
  p $energized.count
end

# starting point of maze.
$routes = [[:right, 0, 0]]

loop do
  step = $routes.pop
  limits(step[1], step[2])
  if limits(step[1], step[2])
    run = step.first
    case run
    when :right
      right(step[1], step[2])
    when :left
      left(step[1], step[2])
    when :up
      up(step[1], step[2])
    when :down
      down(step[1], step[2])
    end
  end
  break if $routes.empty?
end
p $energized.count
