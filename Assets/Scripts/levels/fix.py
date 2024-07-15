import json
# grab json
path = "Assets/Scripts/levels/level2.json"
# open the file
with open(path, 'r') as file:
  data = json.load(file)
  #compress it onto a single line
  data = json.dumps(data)
  #replace all " with \"
  data = data.replace('"', '\\"')
  #print it
  
  print(data)