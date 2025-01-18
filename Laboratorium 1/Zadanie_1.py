def split_parcels(weights, max_weight):
    weights.sort(reverse=True)
    lots = []

    for parcel_weight in weights:
        placed = False

        for lot in lots:
            if sum(lot) + parcel_weight <= max_weight:
                lot.append(parcel_weight)
                placed = True
                break
        
        if not placed:
            lots.append([parcel_weight])
    
    return lots, len(lots)


weights = [8, 10, 13, 7, 6, 11, 9, 5, 12, 6, 7, 3, 5]
max_weight = 50

lots, number_of_lots = split_parcels(weights=weights, max_weight=max_weight)

print(f"Number of lots: {number_of_lots}")

for i, lot in enumerate(lots, 1):
    print(f"lot {i} ({sum(lot)} kg): {lot}")
