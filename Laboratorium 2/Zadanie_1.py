import re
from collections import Counter

STOP_WORDS = ["i", "a", "the", "to", "is", "and"]


def process_text(text):
    paragraphs = text.strip().split("\n")
    paragraphs_count = len([p for p in paragraphs if p.strip()])
    
    sentences = re.split(r'[.!?]', text)
    sentences_count = len([s for s in sentences if s.strip()])
    
    words = re.findall(r'\b\w+\b', text.lower())
    filtered_words = [word for word in words if word not in STOP_WORDS]
    
    words_appearance = Counter(filtered_words)
    most_common_words = words_appearance.most_common(5)
    
    reversed_words = map(lambda w: w[::-1] if w.lower().startswith('a') else w, words)
    transformed_text = ' '.join(reversed_words)
    
    return {
        "words_count": len(words),
        "sentences_count": sentences_count,
        "paragraphs_count": paragraphs_count,
        "most_common_words": most_common_words,
        "transformed_text": transformed_text
    }


text = """Trust Wallet Token (TWT) is a utility token that is used to incentivize users of the Trust Wallet platform. 
It is designed to make cryptocurrencies more accessible and is part of Trust Wallet's goal to build a seamless Web3 gateway and an open ecosystem for secure and decentralized transactions. 
TWT tokens can be used for various purposes within the Trust Wallet platform.
"""

result = process_text(text=text)
print("Words count: ", result["words_count"])
print("Sentences count: ", result["sentences_count"])
print("Paragraphs count: ", result["paragraphs_count"])
print("Most common words: ", result["most_common_words"])
print("Transformed text: ", result["transformed_text"])
